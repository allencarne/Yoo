using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    protected override void EnemyIdleState()
    {
        // Animation
        enemyAnimator.Play("Idle");

        // If Dummy position is not the starting position, idle time increases every second
        if (enemyRB.position != startingPosition)
        {
            idleTime += 1 * Time.deltaTime;
        }

        // If idle time is over 10 seconds, reset
        if (idleTime >= 5)
        {
            state = EnemyState.reset;
        }
    }

    protected override void EnemyResetState()
    {
        // Animation
        enemyAnimator.Play("Reset");

        idleTime = 0;
    }

    protected override void EnemyHurtState(float damage)
    {
        base.EnemyHurtState(damage);

        idleTime = 0;
    }

    public void AE_Reset()
    {
        enemyRB.position = startingPosition;
        state = EnemyState.spawn;
    }
}
