using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    float dummySpawnAnimationDuration = .5f;

    protected override void EnemySpawnState()
    {
        StartCoroutine(WaitForSpawn());
    }

    IEnumerator WaitForSpawn()
    {
        enemyAnimator.Play("Spawn");

        yield return new WaitForSeconds(dummySpawnAnimationDuration);

        enemyState = EnemyState.idle;
    }
}
