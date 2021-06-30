using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("End Level");
            GameManager.Instance.GoToNexLevel(GetNextScene());
        }
    }


    private int GetNextScene()
    {
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex+1);
        return SceneManager.GetActiveScene().buildIndex + 1;

    }
}
