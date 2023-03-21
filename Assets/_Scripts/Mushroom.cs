using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        // Animate
        enemyAnimator.Play("Slam");
    }
}
