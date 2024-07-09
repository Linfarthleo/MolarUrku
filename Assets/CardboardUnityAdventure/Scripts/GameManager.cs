using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image imgFade;

    void Start()
    {
        // Comenzar con la imagen totalmente opaca (visible)
        imgFade.canvasRenderer.SetAlpha(1.0f);
        // Llamar a FadeOut después de un breve retardo, para dar tiempo a inicializaciones
        Invoke("FadeOut", 1);
    }

    // Función para hacer fade out (hacer transparente)
    private void FadeOut()
    {
        imgFade.CrossFadeAlpha(0, 2, false);
    }

    // Función para hacer fade in (hacer opaco)
    public void FadeIn()
    {
        // Asegurarse de que la imagen es completamente visible antes de iniciar el fade in
        imgFade.canvasRenderer.SetAlpha(0f);
        imgFade.CrossFadeAlpha(1, 2, false);
    }

    // Puedes llamar a esta función desde otros scripts cuando necesites iniciar un fade in
    public void StartFadeIn()
    {
        Invoke("FadeIn", 1);  // Puede ajustarse el retardo según sea necesario
    }
}
