using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] GameObject biteTelegraph;
    [SerializeField] float biteCoolDown;

    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        if (canAttack)
        {
            // Prevents from running more than once
            canAttack = false;

            // Animation
            enemyAnimator.Play("Bite");

            // Prevents enemy from being moved while casting
            enemyRB.isKinematic = true;

            // Attack Prefab
            Instantiate(biteTelegraph, transform.position, enemyAimer.rotation);

            // CoolDown
            StartCoroutine(BiteCoolDown());
        }
    }

    IEnumerator BiteCoolDown()
    {
        yield return new WaitForSeconds(biteCoolDown);

        canAttack = true;
    }
}