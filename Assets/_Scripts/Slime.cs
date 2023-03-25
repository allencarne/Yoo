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

        // Animation
        enemyAnimator.Play("Bite");

        if (canAttack)
        {
            canAttack = false;

            Instantiate(biteTelegraph, transform.position, enemyAimer.rotation);

            StartCoroutine(BiteCoolDown());
        }
    }

    IEnumerator BiteCoolDown()
    {
        yield return new WaitForSeconds(biteCoolDown);

        canAttack = true;
    }
}
