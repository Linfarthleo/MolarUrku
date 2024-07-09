using System.Collections;
using UnityEngine;

public class CorrectSelection : MonoBehaviour
{
    public AudioClip successSound;
    public Material blinkMaterial;  // Material para el parpadeo
    public GameObject objectToDisappear;  // El otro objeto que también desaparecerá
    public MissionController missionController;  // Referencia al controlador de la misión


    private AudioSource audioSource;
    private Material originalMaterial;
    private Material secondObjectOriginalMaterial;  // Para guardar el material original del segundo objeto




    private void Start()
    {
        // Guardar el material original del objeto principal
        originalMaterial = GetComponent<Renderer>().material;

        // Guardar el material original del segundo objeto, si existe
        if (objectToDisappear != null)
            secondObjectOriginalMaterial = objectToDisappear.GetComponent<Renderer>().material;

        // Asegurar que el objeto tiene un componente AudioSource y asignar el clip de sonido
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Añade un AudioSource si no existe
        }
        audioSource.clip = successSound;
    }

    public void OnPointerClickXR()
{
    Debug.Log("OnPointerClickXR called on " + gameObject.name);

    if (audioSource.clip != null)
    {
        audioSource.Play();
        Debug.Log("Playing success sound.");
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
    Debug.Log("Starting BlinkAndDisable Coroutine.");
    
        Renderer renderer = GetComponent<Renderer>();
        Renderer secondRenderer = objectToDisappear != null ? objectToDisappear.GetComponent<Renderer>() : null;

        // Parpadear en verde ambos objetos
        if (secondRenderer != null)
        {
            SetMaterials(renderer, secondRenderer, blinkMaterial);
            yield return new WaitForSeconds(0.3f);
            SetMaterials(renderer, secondRenderer, originalMaterial, secondObjectOriginalMaterial);
            yield return new WaitForSeconds(0.3f);
            SetMaterials(renderer, secondRenderer, blinkMaterial);
            yield return new WaitForSeconds(0.3f);
             SetMaterials(renderer, secondRenderer, blinkMaterial);
            yield return new WaitForSeconds(0.3f);
            SetMaterials(renderer, secondRenderer, originalMaterial, secondObjectOriginalMaterial);
            yield return new WaitForSeconds(0.3f);

        }

        // Restaurar el material original y desactivar ambos objetos
        SetMaterials(renderer, secondRenderer, originalMaterial, secondObjectOriginalMaterial);
        gameObject.SetActive(false);
        if (objectToDisappear != null)
            objectToDisappear.SetActive(false);
    }

    void SetMaterials(Renderer firstRenderer, Renderer secondRenderer, Material firstMat, Material secondMat = null)
    {
        firstRenderer.material = firstMat;
        if (secondRenderer != null)
            secondRenderer.material = secondMat ?? firstMat;
    }
}
