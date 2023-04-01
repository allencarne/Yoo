using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

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
        Instantiate(playerPrefab);
    }

    void SpawnPlayer()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);
        Instantiate(playerPrefab);
    }
}
