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

    void SpawnPlayer()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);

        var player = Instantiate(playerPrefab);
        virtualCam.Follow = player.transform;
        canSpawn = true;
    }
}
