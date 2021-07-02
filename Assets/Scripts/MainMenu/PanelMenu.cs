using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : MonoBehaviour , IUiSoundPlayer
{

    #region Field Declerations
    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    [Header("Animation")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator animator;
    

    #endregion

    #region UI Event Functions

    public void PanelSet()
    {
        ClickSound();
        panel.SetActive(true);
    }

    public void PanelClose()
    {
        ClickSound();
        animator.SetTrigger("Close");
    }

    public void Disable()
    {
        panel.SetActive(false);
    }

    public void ClickSound()
    {
        audioManager.Play("UiClick");
    }
    #endregion
}
