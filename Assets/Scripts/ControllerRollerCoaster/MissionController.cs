using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour
{
    public int totalCorrectRequired = 3;
    private int currentCorrectCount = 0;
    public int missionIndex;  // 0 for first mission, 1 for second mission, 2 for third mission

    public void RegisterCorrectSelection()
    {
        if (++currentCorrectCount >= totalCorrectRequired)
        {
            MissionCompleted();
        }
    }

    void MissionCompleted()
    {
        switch (missionIndex)
        {
            case 0:
                GameStateManager.Instance.FirstMissionCompleted = true;
                break;
            case 1:
                GameStateManager.Instance.SecondMissionCompleted = true;
                break;
            case 2:
                GameStateManager.Instance.ThirdMissionCompleted = true;  // Mark the third mission as completed
                break;
        }
        GameStateManager.Instance.MissionCompleted = true;
        Invoke("CompleteMission", 2.0f);
    }

    void CompleteMission()
    {
        SceneManager.LoadScene("WorldOneScene");  // Assumes this is the main scene for the rollercoaster
    }
}
