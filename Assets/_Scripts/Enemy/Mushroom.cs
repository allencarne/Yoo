using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] GameObject slamTelegraph;
    [SerializeField] float slamCoolDown;

    public static event System.Action OnMushroomDeath;

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
            //enemyRB.isKinematic = true;

            // Attack Prefab
            Instantiate(slamTelegraph, transform.position, enemyAimer.rotation, transform);

            // CoolDown
            StartCoroutine(SlamCoolDown());
        }
    }

    IEnumerator SlamCoolDown()
    {
        yield return new WaitForSeconds(slamCoolDown);

        canAttack = true;
    }

    protected override void EnemyHurtState(float damage)
    {
        base.EnemyHurtState(damage);

        if (enemyHealth <= 0)
        {
            OnMushroomDeath?.Invoke();
        }
    }
}
