using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginner : Player
{
    protected override void PlayerIdleState()
    {
        // Animate Body
        animator.Play("Idle");
        // Animate Sword
        animator.Play("Idle", 1);
        // Animate Bow
        animator.Play("Idle", 2);
        // Animate Staff
        animator.Play("Idle", 3);
        // Animate Dagger
        animator.Play("Idle", 4);

        // Tranitions
        MoveKeyPressed();
        //BasicAttackKeyPressed();
    }

    protected override void PlayerRunState()
    {
        // Animate Body
        animator.Play("Run");
        // Animate Sword
        animator.Play("Run", 1);
        // Animate Bow
        animator.Play("Run", 2);
        // Animate Staff
        animator.Play("Run", 3);
        // Animate Dagger
        animator.Play("Run", 4);

        // Set idle Animation after move
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Input
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * SO_Player.movementSpeed;

        // Transitions
        NoMoveKeyPressed();
        //BasicAttackKeyPressed();
    }
}
