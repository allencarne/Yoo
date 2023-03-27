using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] GameObject slamTelegraph;
    [SerializeField] float slamCoolDown;

    protected override void EnemyAttackState()
    {
        base.EnemyAttackState();

        if (canAttack)
        {
            // Prevents from running more than once
            canAttack = false;

            // Animation
            enemyAnimator.Play("Slam");

            // Prevents enemy from being moved while casting
            enemyRB.isKinematic = true;

            // Attack Prefab
            Instantiate(slamTelegraph, transform.position, enemyAimer.rotation);

            // CoolDown
            StartCoroutine(SlamCoolDown());
        }
    }

    IEnumerator SlamCoolDown()
    {
        yield return new WaitForSeconds(slamCoolDown);

        canAttack = true;
    }
}
