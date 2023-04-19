using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Vector3 size;

    int enemyCount;
    public int maxEnemyCount;

    private void OnEnable()
    {
        Snail.OnSnailDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        Snail.OnSnailDeath -= OnEnemyDeath;
    }

    void Update()
    {
        if (enemyCount < maxEnemyCount)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        enemyCount++;

        Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    void OnEnemyDeath()
    {
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));

        enemyCount--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
