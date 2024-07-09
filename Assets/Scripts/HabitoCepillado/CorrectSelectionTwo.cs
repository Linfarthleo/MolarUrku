using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CorrectSelectionTwo : MonoBehaviour
{
    public AudioClip successSound;
    public Material blinkMaterial;  // Material para el parpadeo del objeto 3D
    public Color blinkColor = Color.green;  // Color para el parpadeo de la imagen
    public GameObject objectToDisappear;  // Este será el objeto con el componente Image
    public MissionController missionController;  // Referencia al controlador de la misión

    private AudioSource audioSource;
    private Material originalMaterial;  // Material original del objeto 3D
    private Color originalImageColor;  // Color original de la imagen
    private Renderer objectRenderer;  // Renderer del objeto 3D
    private Image imageComponent;  // Componente Image del objeto a desaparecer

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on " + gameObject.name);
        }

        if (objectToDisappear != null)
        {
            imageComponent = objectToDisappear.GetComponent<Image>();
            if (imageComponent != null)
            {
                originalImageColor = imageComponent.color;
            }
            else
            {
                Debug.LogError("Image component not found on " + objectToDisappear.name);
            }
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = successSound;
    }

    public void OnPointerClickXR()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No success sound has been assigned to the AudioClip.");
        }

        if (missionController != null)
        {
            missionController.RegisterCorrectSelection();
        }
        else
        {
            Debug.LogError("MissionController not assigned on " + gameObject.name);
        }

        StartCoroutine(BlinkAndDisable());
    }

    IEnumerator BlinkAndDisable()
    {
        int blinkCount = 5;
        for (int i = 0; i < blinkCount; i++)
        {
            if (objectRenderer != null)
            {
                objectRenderer.material = blinkMaterial;
            }
            if (imageComponent != null)
            {
                imageComponent.color = blinkColor;
            }
            yield return new WaitForSeconds(0.3f);

            if (objectRenderer != null)
            {
                objectRenderer.material = originalMaterial;
            }
            if (imageComponent != null)
            {
                imageComponent.color = originalImageColor;
            }
            yield return new WaitForSeconds(0.3f);
        }

        gameObject.SetActive(false);
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
        }
    }
}
