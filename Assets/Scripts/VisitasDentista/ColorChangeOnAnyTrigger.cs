using UnityEngine;
using UnityEngine.UI;

public class ColorChangeOnAnyTrigger : MonoBehaviour
{
    public Image targetImage; // Arrastra aquí el componente Image en el Inspector.
    public Color triggerColor = Color.red; // Color durante el trigger.
    public Vector3 expandedScale = new Vector3(1.2f, 1.2f, 1.2f); // Escala ampliada.
    private Color originalColor; // Color original de la imagen.
    private Vector3 originalScale; // Escala original de la imagen.

    void Start()
    {
        if (targetImage != null)
        {
            originalColor = targetImage.color; // Guarda el color original.
            originalScale = targetImage.transform.localScale; // Guarda la escala original.
        }
        else
        {
            Debug.LogError("No se ha asignado Image al script.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Cambiar el color de la imagen al color de trigger y aumentar su tamaño
        Debug.Log("Trigger iniciado con: " + other.gameObject.name);
        if (targetImage != null)
        {
            targetImage.color = triggerColor;
            targetImage.transform.localScale = expandedScale;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Restaurar el color original de la imagen y su tamaño cuando el trigger termine
        Debug.Log("Trigger terminado con: " + other.gameObject.name);
        if (targetImage != null)
        {
            targetImage.color = originalColor;
            targetImage.transform.localScale = originalScale;
        }
    }
}
