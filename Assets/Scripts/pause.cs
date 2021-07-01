using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    #region Field Field Declarations
    [Header("Pausemenu")]
    [SerializeField] private Canvas ControlUI;
    public static bool isPaused;

    #endregion

    #region PauseMenu

    private void Awake()
    {
        isPaused = false;
    }


    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        ControlUI.gameObject.SetActive(false);
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        ControlUI.gameObject.SetActive(true);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    #endregion
}
