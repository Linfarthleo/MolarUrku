using UnityEngine;

public class PendulumScript : MonoBehaviour
{
    public bool isCollidingWithA = false;
    public bool isCollidingWithB = false;

    void Update()
    {
        // Lógica para mover el péndulo
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pivot")
        {
            isCollidingWithA = true;
            isCollidingWithB = false;
        }
        else if (other.gameObject.name == "PivotB")
        {
            isCollidingWithB = true;
            isCollidingWithA = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Pivot")
        {
            isCollidingWithA = false;
        }
        else if (other.gameObject.name == "PivotB")
        {
            isCollidingWithB = false;
        }
    }
}
