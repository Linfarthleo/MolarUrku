using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MissionObjectSelector : MonoBehaviour
{
    public CinemachineDollyCart dollyCart;
    public float stopPosition;
    public string nextSceneName;

    private bool isSelectable = false;

    void Update()
    {
        CheckSelectionState();
    }

    private void CheckSelectionState()
    {
        if (dollyCart.m_Position >= stopPosition && !GameStateManager.Instance.MissionStarted)
        {
            isSelectable = true;
        }
        else if (dollyCart.m_Position < stopPosition || GameStateManager.Instance.MissionStarted)
        {
            isSelectable = false;
        }
    }

    public void OnPointerClickXR()
    {
        if (isSelectable)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        GameStateManager.Instance.MissionStarted = true;
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnDisable()
    {
        isSelectable = false;
    }
}
