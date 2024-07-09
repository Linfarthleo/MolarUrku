using UnityEngine;
using UnityEngine.UI; // Importa el espacio de nombres para trabajar con UI.

public class ColorChangeOnCollision : MonoBehaviour
{
    public Image targetImage; // Arrastra aquí el componente Image en el Inspector.
    public Color collisionColor = Color.red; // Color durante la colisión.
    private Color originalColor; // Color original de la imagen.

    void Start()
    {
        if (targetImage != null)
        {
            originalColor = targetImage.color; // Guarda el color original.
        }
        else
        {
            Debug.LogError("No se ha asignado Image al script.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cambia el color de la imagen cuando el objeto colisiona.
        if (targetImage != null)
        {
            targetImage.color = collisionColor;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Restablece el color original cuando la colisión termina.
        if (targetImage != null)
        {
            targetImage.color = originalColor;
        }
    }
}
