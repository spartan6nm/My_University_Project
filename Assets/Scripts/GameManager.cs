using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Field Field Declarations

    [Header("Pausemenu")]
    [SerializeField] private AudioManager audioManager;

    [Header("Pausemenu")]
    [SerializeField] private Canvas ControlUI;


    [Header("PlayerHealth")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float playerMaxhealth;
    private float PlayerHealth;
    [SerializeField] private float fixedDamage;
    [SerializeField] private float playerSpawnTime;
    [SerializeField] private Transform LastSpawnPositiin;
    [SerializeField] private GameObject PlayerPrefab;

    #endregion


    #region Singleton & Unity Functions
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        StartHealth();
        EventBroker.PlayerHited += SetHealth;
        EventBroker.SpawnPositionChange += spawnPositionChanged;

        PlayerPrefs.SetInt("resumelevel", SceneManager.GetActiveScene().buildIndex);


        //DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        audioManager.Play("SoundTrack");

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioManager.Play("Dialog1");
        }
    }

    private void OnDisable()
    {
        EventBroker.PlayerHited -= SetHealth;
        EventBroker.SpawnPositionChange -= spawnPositionChanged;
    }

    #endregion


    #region Player Health
    private void StartHealth()
    {
        healthSlider.maxValue = playerMaxhealth;
        healthSlider.value = playerMaxhealth;
        PlayerHealth = playerMaxhealth;
    }


    private void SetHealth()
    {
        audioManager.Play("PlayerHit");
        PlayerHealth -= fixedDamage;
        healthSlider.value = PlayerHealth;
        CheckHealth();

    }

    private void CheckHealth()
    {
        if(PlayerHealth <= 0)
        {
            PlayerDied();
        }
    }
    #endregion


    #region PlayerSpawn and Next level

    public void PlayerDied()
    {
        audioManager.Play("PlayerDied");
        PlayerPrefab.SetActive(false);
        StartCoroutine(SpawnDelay());
    }

    public void GoToNexLevel(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }


    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(playerSpawnTime);
        Spawn();
    }

    private void Spawn()
    {
        PlayerPrefab.transform.position = LastSpawnPositiin.position;
        PlayerPrefab.GetComponent<SpriteRenderer>().color = Color.white;
        StartHealth();
        PlayerPrefab.SetActive(true);
        audioManager.Play("RespawnDialog"); 
    }

    private void spawnPositionChanged(Transform spawnPosition)
    {
        LastSpawnPositiin = spawnPosition;
    }


    #endregion



}
