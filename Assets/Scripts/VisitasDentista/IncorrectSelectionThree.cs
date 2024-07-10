using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IncorrectSelectionThree : MonoBehaviour
{
    public AudioClip errorSound;
    public Image targetImage;
    public Color blinkColor = Color.red;
    private Color originalColor;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = errorSound;
        originalColor = targetImage.color;  // Asegúrate de que targetImage está asignado en el Inspector
    }

    public void ActivateIncorrectResponse()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        StartCoroutine(BlinkImage());
    }

    IEnumerator BlinkImage()
    {
        int blinkCount = 5;
        for (int i = 0; i < blinkCount; i++)
        {
            targetImage.color = blinkColor;
            yield return new WaitForSeconds(0.3f);
            targetImage.color = originalColor;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
