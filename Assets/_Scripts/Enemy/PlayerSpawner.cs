using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CinemachineVirtualCamera virtualCam;

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
        var player = Instantiate(playerPrefab);
        virtualCam.Follow = player.transform;
    }

    void SpawnPlayer()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(5);

        var player = Instantiate(playerPrefab);
        virtualCam.Follow = player.transform;
    }
}
