using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool MissionStarted { get; set; }
    public bool MissionCompleted { get; set; }
    public bool FirstMissionCompleted { get; set; }
    public bool SecondMissionCompleted { get; set; }

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
