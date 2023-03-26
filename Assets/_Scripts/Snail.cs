using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    [SerializeField] GameObject shmackTelegraph;
    [SerializeField] float shmackCoolDown;

    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        if (canAttack)
        {
            // Prevents from running more than once
            canAttack = false;

            // Animation
            enemyAnimator.Play("Shmack");

            // Prevents enemy from being moved while casting
            enemyRB.isKinematic = true;

            // Attack Prefab
            Instantiate(shmackTelegraph, transform.position, enemyAimer.rotation);

            // CoolDown
            StartCoroutine(ShmackCoolDown());
        }
    }

    IEnumerator ShmackCoolDown()
    {
        yield return new WaitForSeconds(shmackCoolDown);

        canAttack = true;
    }
}
