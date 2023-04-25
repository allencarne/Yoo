using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than once instance of PlayerManager found!");
            return;
        }

        instance = this;
    }

    #endregion

    public GameObject playerPrefab;
    GameObject playerInstance;
    public PlayerScriptableObject player_SO;

    [SerializeField] CinemachineVirtualCamera virtualCam;

    bool canSpawn = true;

    private void OnEnable()
    {
        Player.OnPlayerDeath += SpawnPlayer;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= SpawnPlayer;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        // Testing 
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = .7f;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale = .5f;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Time.timeScale = .3f;
        }
    }

    void SpawnPlayer()
    {
        if (playerInstance)
        {
            Destroy(playerInstance, .5f);
        }
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);

        playerInstance = Instantiate(playerPrefab);
        virtualCam.Follow = playerInstance.transform;
        canSpawn = true;
    }
}
