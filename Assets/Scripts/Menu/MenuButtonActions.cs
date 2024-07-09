using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonActions : MonoBehaviour
{
    public GameObject currentButtons; // Contenedor para los botones actuales
    public GameObject newButtons; // Contenedor para los nuevos botones que aparecerán
    public ParticleSystem transitionEffect; // Sistema de partículas para el efecto de transición

    private void Start()
    {
        // Asegurarse de que los nuevos botones están desactivados inicialmente
        if (newButtons != null)
            newButtons.SetActive(false);
    }

    public void OnPointerEnterXR()
    {
        Debug.Log("Pointer Entered: " + gameObject.name);
    }

    public void OnPointerExitXR()
    {
        Debug.Log("Pointer Exited: " + gameObject.name);
    }

    public void OnPointerClickXR()
    {
        Debug.Log("Pointer Clicked: " + gameObject.name);

        if (gameObject.name == "PlayButton")
        {
            Debug.Log("Play button clicked - Triggering transition effect!");
            StartCoroutine(ActivateNewButtonsAfterEffect());
        }

        if (gameObject.name == "ExitButton")
        {
            Debug.Log("Exit button clicked - Exit the game!");
            Application.Quit();
        }
    }

    private IEnumerator ActivateNewButtonsAfterEffect()
{
    // Iniciar el sistema de partículas
    transitionEffect.Play();
    Debug.Log("Particle system started.");

    // Activar los nuevos botones inmediatamente después de iniciar las partículas
    if (newButtons != null)
    {
        newButtons.SetActive(true);
        Debug.Log("New buttons activated immediately.");
    }

    // Desactivar los botones actuales
    if (currentButtons != null)
    {
        currentButtons.SetActive(false);
        Debug.Log("Current buttons deactivated.");
    }

    // Esperar que el sistema de partículas complete su ciclo (opcional si necesita hacer algo después)
    yield return new WaitForSeconds(transitionEffect.main.duration);

    // Si necesita realizar alguna acción después de que las partículas terminen, colóquela aquí
    Debug.Log("Particle system finished.");
}


}
