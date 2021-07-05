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


    [Header("Sharing")]
    [SerializeField] private string gameName;


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


    #region Sharing Code


#if UNITY_ANDROID

    public void ShowToast()
    {
        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");

        //create an object array of 3 size
        object[] toastParams = new object[3];

        //create a class reference of unity player activity
        AndroidJavaClass unityActivity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        //set the first object in the array 
        //as current activity reference
        toastParams[0] =
        unityActivity.GetStatic<AndroidJavaObject>("currentActivity");

        //set the second object in the array 
        //as the CharSequence to be displayed
        toastParams[1] = "Sharing";

        //set the third object in the array
        //as the duration of the toast from
        toastParams[2] = toastClass.GetStatic<int>("LENGTH_LONG");

        AndroidJavaObject toastObject =
               toastClass.CallStatic<AndroidJavaObject>
                            ("makeText", toastParams);

        toastObject.Call("show");
    }

    public void Share()
    {
        ClickSound();

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        string shareSubject = "Check out " + gameName + ", jump and kill in this amazing 2D platformer game";


        string shareMessage = "i have reached level " +
            PlayerPrefs.GetInt("level").ToString() +
            " in " + gameName + "\n" +
            " you can download it from: \n" +
            "https://spartan-nm.itch.io/ario";


        //set the type 
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

        AndroidJavaClass unity = new
                AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity =
                     unity.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject chooser =
        intentClass.CallStatic<AndroidJavaObject>("createChooser",
                     intentObject, "Share Ario with Friends");
        currentActivity.Call("startActivity", chooser);
    }
    //..
#endif



    #endregion
}
