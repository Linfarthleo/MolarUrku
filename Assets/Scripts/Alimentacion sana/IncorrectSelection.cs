using System.Collections;
using UnityEngine;

public class IncorrectSelection : MonoBehaviour
{
    public AudioClip errorSound;
    public Material errorMaterial;
    private Material originalMaterial;
    private AudioSource audioSource;

    void Start()
    {
        // Asegurar que el objeto tiene un componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = errorSound;

        // Guardar el material original del objeto
        originalMaterial = GetComponent<Renderer>().material;
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
        Renderer renderer = GetComponent<Renderer>();

        // Cambiar al material de error y luego regresar al original varias veces
        renderer.material = errorMaterial;
        yield return new WaitForSeconds(0.5f);
        renderer.material = originalMaterial;
        yield return new WaitForSeconds(0.5f);
        renderer.material = errorMaterial;
        yield return new WaitForSeconds(0.5f);
        renderer.material = originalMaterial;
    }
}
