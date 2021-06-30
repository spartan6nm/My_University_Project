using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMenu : MonoBehaviour
{

    #region Field Declerations
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator animator;
    

    #endregion

    #region UI Event Functions

    public void PanelSet()
    {
        panel.SetActive(true);
    }

    public void PanelClose()
    {
        animator.SetTrigger("Close");
    }

    public void Disable()
    {
        panel.SetActive(false);
    }
    #endregion
}
