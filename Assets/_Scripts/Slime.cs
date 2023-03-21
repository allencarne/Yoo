using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        // Animate
        enemyAnimator.Play("Bite");
    }
}
