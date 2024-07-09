using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class RollerCoasterController : MonoBehaviour
{
    public CinemachineDollyCart dollyCart;
    public float[] stopPositions;
    private bool[] missionsTriggered;

    void Start()
    {
        missionsTriggered = new bool[stopPositions.Length];
    }

    void Update()
    {
        HandleMissionActivations();
    }

    private void HandleMissionActivations()
    {
        for (int i = 0; i < stopPositions.Length; i++)
        {
            if (dollyCart.m_Position >= stopPositions[i] && !missionsTriggered[i])
            {
                StopRollerCoaster(i);
            }
        }
    }

    private void StopRollerCoaster(int index)
    {
        dollyCart.m_Speed = 0;
        missionsTriggered[index] = true;
        GameStateManager.Instance.MissionStarted = false; // Restablecer para nuevas misiones
    }
}
