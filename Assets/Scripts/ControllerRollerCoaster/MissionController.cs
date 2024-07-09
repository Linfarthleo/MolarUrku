using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    public int totalCorrectRequired = 3;
    private int currentCorrectCount = 0;
    public int missionIndex;  // 0 for first mission, 1 for second mission

    public void RegisterCorrectSelection()
    {
        if (++currentCorrectCount >= totalCorrectRequired)
        {
            MissionCompleted();
        }
    }

    void MissionCompleted()
    {
        if (missionIndex == 0)
        {
            GameStateManager.Instance.FirstMissionCompleted = true;  // Mark the first mission as completed
        }
        else if (missionIndex == 1)
        {
            GameStateManager.Instance.SecondMissionCompleted = true;  // Mark the second mission as completed
        }

        GameStateManager.Instance.MissionCompleted = true;  // General flag if needed
        Invoke("CompleteMission", 2.0f);
    }

    void CompleteMission()
    {
        // Load the rollercoaster scene again to continue the journey
        SceneManager.LoadScene("WorldOneScene");
    }
}
