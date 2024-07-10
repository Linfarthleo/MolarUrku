using System.Collections;
using UnityEngine;
using Cinemachine;

public class ContinueRollerCoaster : MonoBehaviour
{
    public CinemachineDollyCart dollyCart;
    public float[] resumePositions = new float[] { 36f, 69f, 157f }; // Agregado la posición para la tercera misión

    void OnEnable()
    {
        Debug.Log("Enabled ContinueRollerCoaster");
        Debug.Log("Current dollyCart position: " + dollyCart.m_Position);

        int positionIndex = DetermineResumePosition();
        if (positionIndex != -1)
        {
            Debug.Log("Resuming from position: " + resumePositions[positionIndex]);
            dollyCart.m_Position = resumePositions[positionIndex];
            StartCoroutine(ResumeAfterDelay(2f));
        }
        else
        {
            Debug.Log("No mission completed flags set, not resuming from a specific position.");
        }
    }

    private int DetermineResumePosition()
    {
        // Check from the last mission to the first to capture the latest completed position
        if (GameStateManager.Instance.ThirdMissionCompleted) // Asumiendo que existe un flag para la tercera misión
        {
            return 2; // Index for the third mission's resume position
        }
        else if (GameStateManager.Instance.SecondMissionCompleted)
        {
            return 1; // Index of the second mission's resume position
        }
        else if (GameStateManager.Instance.FirstMissionCompleted)
        {
            return 0; // Index of the first mission's resume position
        }
        return -1; // No missions completed
    }

    IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dollyCart.m_Speed = 2.0f; // Ensure this is the desired speed
        Debug.Log("Rollercoaster resuming at speed: " + dollyCart.m_Speed);
    }
}
