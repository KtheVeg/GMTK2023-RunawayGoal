using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField]
    GameObject ConfimationPanel;
    [SerializeField]
    GameFlow gameFlow;
    [SerializeField]
    string sceneName;
    [SerializeField]
    GameObject controlsUI;
    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SkipTime()
    {
        if (!gameFlow.isRoaming) return;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameFlow.EnterStationaryMode();
    }
    public void Quit()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void ConfirmQuit()
    {
        ConfimationPanel.SetActive(true);
    }
    public void CancelQuit()
    {
        ConfimationPanel.SetActive(false);
    }
    public void Retry()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ToggleControls()
    {
        controlsUI.SetActive(!controlsUI.activeSelf);
    }
}
