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

        // Animation
        enemyAnimator.Play("Slam");

        if (canAttack)
        {
            canAttack = false;

            Instantiate(slamTelegraph, transform.position, enemyAimer.rotation);

            StartCoroutine(SlamCoolDown());
        }
    }

    IEnumerator SlamCoolDown()
    {
        yield return new WaitForSeconds(slamCoolDown);

        canAttack = true;
    }
}
