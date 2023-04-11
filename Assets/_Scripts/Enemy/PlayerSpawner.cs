using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
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
