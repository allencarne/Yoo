using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slayer : Player
{
    [Header("FireSlash")]
    [SerializeField] GameObject fireSlashPrefab;
    [SerializeField] float fireSlashCoolDown;
    [SerializeField] bool isFireSlashActive;

    protected override void PlayerBasicAttackState()
    {
        if (canBasicAttack)
        {
            canBasicAttack = false;

            // Animation
            animator.Play("Sword Swing Right");
            animator.Play("Sword Swing Right", 1);

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer());
            StartCoroutine(FireSlashCastTime());
            StartCoroutine(FireSlashAnimationDuration());
            StartCoroutine(FireSlashCoolDown());
        }

        if (isFireSlashActive)
        {
            isFireSlashActive = false;

            canSlide = true;

            Instantiate(fireSlashPrefab, transform.position, aimer.rotation);
        }
    }

    IEnumerator FireSlashCastTime()
    {
        yield return new WaitForSeconds(.3f);

        isFireSlashActive = true;
    }

    IEnumerator FireSlashAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        state = PlayerState.Idle;
    }

    IEnumerator FireSlashCoolDown()
    {
        yield return new WaitForSeconds(fireSlashCoolDown);

        canBasicAttack = true;
    }



    IEnumerator UnpauseAimer()
    {
        yield return new WaitForSeconds(.3f);

        AimIndicator.pauseDirection = false;
    }
}
