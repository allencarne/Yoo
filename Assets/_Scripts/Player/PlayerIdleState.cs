using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Idle State");

        // Play Idle Animation
        player.animator.Play("Idle");
        player.animator.Play("Idle", 1);
        player.animator.Play("Idle", 2);
        player.animator.Play("Idle", 3);
        player.animator.Play("Idle", 4);
        player.animator.Play("Idle", 5);
        player.animator.Play("Idle", 6);
        player.animator.Play("Idle", 7);
        player.animator.Play("Idle", 8);
        player.animator.Play("Idle", 9);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // State Transition - Move
        if (player.AnyMoveKeyPressed)
        {
            player.ChangeState(player.moveState);
        }

        // if Attacking, change to attack state
        //player.ChangeState(player.basicAttackState);

        // if Hurt, change to hurt state
        //player.ChangeState(player.hurtState);
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
