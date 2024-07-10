using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Animator pendulumAnimator;
    public PendulumScript pendulumScript;
    public MissionController missionController;
    public IncorrectSelectionThree incorrectSelection;  // Referencia al script de selección incorrecta.
    private AudioSource audioSource;  // Asegúrate de que el componente AudioSource con el clip deseado está asignado.
    public AudioClip successSound;   // Arrastra aquí el clip de sonido de éxito.
    public Image targetImage;        // Arrastra aquí la imagen que debe parpadear.
    public Color blinkColor = Color.green;  // Color para el parpadeo, en este caso verde.
    
    private Color originalColor;     // Para almacenar el color original de la imagen.

    void Start()
    {
        if (targetImage != null)
            originalColor = targetImage.color; // Guarda el color original de la imagen.

            // Configuración del AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Añade un AudioSource si no existe
        }
        audioSource.clip = successSound;
    }

    public void OnPointerClickXR()
    {
        // Detiene la animación del péndulo pausando el Animator
        if (pendulumAnimator != null)
        {
            pendulumAnimator.enabled = false;
        }

        Invoke("CheckAnswer", 2f);
    }

    void CheckAnswer()
    {
        // Comprobar si el péndulo está colisionando con el objeto B
        if (pendulumScript.isCollidingWithB)
        {
            // Si la colisión es con el objeto B, registra la selección correcta
            missionController.RegisterCorrectSelection(); // Registrar selección correcta
            PlaySuccessEffects();
        }
        else
        {
            incorrectSelection.ActivateIncorrectResponse();  // Activar la respuesta incorrecta directamente
            ResumePendulum();  // Si la respuesta es incorrecta, reanuda el movimiento del péndulo
        }
        
    }

    void PlaySuccessEffects()
    {
        // Reproducir sonido de error
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Success sound clip is not assigned to AudioSource.");
        }

        if (targetImage != null)
            StartCoroutine(BlinkImage()); // Iniciar parpadeo de la imagen.
    }

    IEnumerator BlinkImage()
    {
        int blinkCount = 8;
        for (int i = 0; i < blinkCount; i++)
        {
            targetImage.color = blinkColor;
            yield return new WaitForSeconds(0.3f);
            targetImage.color = originalColor;
            yield return new WaitForSeconds(0.3f);
        }
    }

    void ResumePendulum()
    {
        // Reanuda la animación del péndulo habilitando el Animator
        if (pendulumAnimator != null)
        {
            pendulumAnimator.enabled = true;
        }

        Debug.Log("Pendulum movement resumed due to incorrect selection.");
    }

}