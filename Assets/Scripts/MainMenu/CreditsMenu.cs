using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{

    #region Field Declerations
    [SerializeField] private GameObject CreditsPanel;
    [SerializeField] private Animator animator;
    

    #endregion

    #region UI Event Functions

    public void CreditsSet()
    {
        CreditsPanel.SetActive(true);
    }

    public void CreditsClose()
    {
        animator.SetTrigger("Close");
    }

    public void Disable()
    {
        CreditsPanel.SetActive(false);
    }
    #endregion
}
