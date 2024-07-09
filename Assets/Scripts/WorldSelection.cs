using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar la carga de escenas

public class WorldSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

        
        if (gameObject.name == "WorldOne")
        {
            // Carga la escena asociada al mundo uno
            SceneManager.LoadScene("WorldOneScene");
        }

        if (gameObject.name == "WorldTwo")
        {
            // Carga la escena asociada al mundo dos
            SceneManager.LoadScene("WorldTwoScene");
        }
    }
}
