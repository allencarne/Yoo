using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        // Animation
        enemyAnimator.Play("Bite");
    }
}
