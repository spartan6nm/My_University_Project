using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Field Field Declarations

    [Header("PlayerHealth")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float playerMaxhealth;
    private float PlayerHealth;
    [SerializeField] private float fixedDamage;
    [SerializeField] private Transform LastSpawnPositiin;
    [SerializeField] private GameObject PlayerPrefab;

    #endregion


    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        StartHealth();
        EventBroker.PlayerHited += SetHealth;
        EventBroker.SpawnPositionChange += spawnPositionChanged;

        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
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
        PlayerHealth -= fixedDamage;
        healthSlider.value = PlayerHealth;
        CheckHealth();
        Debug.Log(PlayerHealth +  " Player Health" );

    }

    private void CheckHealth()
    {
        if(PlayerHealth <= 0)
        {
            Debug.LogError("Player Died");
            PlayerDied();
        }
    }
    #endregion


    #region PlayerSpawn

    public void PlayerDied()
    {
        PlayerPrefab.SetActive(false);
        StartCoroutine(SpawnDelay());
    }


    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(5);
        Spawn();
    }

    private void Spawn()
    {
        PlayerPrefab.transform.position = LastSpawnPositiin.position;
        PlayerPrefab.GetComponent<SpriteRenderer>().color = Color.white;
        StartHealth();
        PlayerPrefab.SetActive(true);
    }

    private void spawnPositionChanged(Transform spawnPosition)
    {
        LastSpawnPositiin = spawnPosition;
        Debug.LogError(LastSpawnPositiin.position);
    }
    #endregion

}
