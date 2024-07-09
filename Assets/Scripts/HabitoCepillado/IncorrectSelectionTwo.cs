using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Importante para trabajar con el componente Image

public class IncorrectSelectionTwo : MonoBehaviour
{
    public AudioClip errorSound;
    public Material errorMaterial; // Material para el parpadeo del objeto 3D
    public Color errorColor = Color.red; // Color para el parpadeo de la imagen
    public GameObject imageObject; // El objeto con el componente Image que también parpadeará

    private AudioSource audioSource;
    private Material originalMaterial; // Material original del objeto 3D
    private Color originalImageColor; // Color original de la imagen
    private Renderer objectRenderer; // Renderer del objeto 3D
    private Image imageComponent; // Componente Image del objeto adicional

    void Start()
    {
        // Configuración del AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Añade un AudioSource si no existe
        }
        audioSource.clip = errorSound;

        // Configuración del Renderer y el material original
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        // Configuración de la imagen y su color original
        if (imageObject != null)
        {
            imageComponent = imageObject.GetComponent<Image>();
            if (imageComponent != null)
            {
                originalImageColor = imageComponent.color;
            }
        }
    }

    public void OnPointerClickXR()
    {
        // Reproducir sonido de error
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Error sound clip is not assigned to AudioSource.");
        }

        // Iniciar la rutina de parpadeo
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        int blinkCount = 3; // Define cuántas veces quieres que parpadee
        for (int i = 0; i < blinkCount; i++)
        {
            // Cambiar al material y color de error
            if (objectRenderer != null)
            {
                objectRenderer.material = errorMaterial;
            }
            if (imageComponent != null)
            {
                imageComponent.color = errorColor;
            }

            yield return new WaitForSeconds(0.5f);

            // Regresar al material y color original
            if (objectRenderer != null)
            {
                objectRenderer.material = originalMaterial;
            }
            if (imageComponent != null)
            {
                imageComponent.color = originalImageColor;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
