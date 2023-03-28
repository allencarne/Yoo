using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : Player
{
    [Header("Variables")]
    [SerializeField] float basicAttackCoolDown;

    protected override void PlayerBasicAttackState()
    {
        // Animation
        animator.Play("Sword Swing Right");
        animator.Play("Sword Swing Right", 1);
        if (canBasicAttack)
        {
            canBasicAttack = false;
            StartCoroutine(BasicAttackAnimationDuration());
            StartCoroutine(BasicAttackCoolDown());
        }
    }

    IEnumerator BasicAttackAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        state = PlayerState.Idle;
    }

    IEnumerator BasicAttackCoolDown()
    {
        yield return new WaitForSeconds(basicAttackCoolDown);

        canBasicAttack = true;
    }
}
