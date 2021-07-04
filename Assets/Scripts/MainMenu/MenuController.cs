using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour , IUiSoundPlayer
{
    #region Field Declarations

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    [Header("UI")]
    [SerializeField] private Button mission1;
    [SerializeField] private Button mission2;
    [SerializeField] private Button mission3;


    #endregion

    #region Unity Functions

    private void Awake()
    {

        if(!PlayerPrefs.HasKey("level") || PlayerPrefs.GetInt("level") == 1)
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("resumelevel", 1);
            mission1.interactable = true;
        }
        else if(PlayerPrefs.GetInt("level") == 2)
        {
            mission1.interactable = true;
            mission2.interactable = true;
        }
        else if(PlayerPrefs.GetInt("level") == 3)
        {
            mission1.interactable = true;
            mission2.interactable = true;
            mission3.interactable = true;
        }
        
    }
    private void Start()
    {
        audioManager.Play("SoundTrack");
    }
    #endregion


    #region UI Functions

    public void PlayStart()
    {
        audioManager.Play("UiClick");
        SceneManager.LoadScene(PlayerPrefs.GetInt("resumelevel"));
    }

    public void Quit()
    {
        audioManager.Play("UiClick");
        Application.Quit();
    }

    public void Play1()
    {
        ClickSound();
        SceneManager.LoadScene(1);
    }
    public void Play2()
    {
        ClickSound();
        SceneManager.LoadScene(2);
    }
    public void Play3()
    {
        ClickSound();
        SceneManager.LoadScene(3);
    }

    public void ClickSound()
    {
        audioManager.Play("UiClick");
    }
    #endregion
}
