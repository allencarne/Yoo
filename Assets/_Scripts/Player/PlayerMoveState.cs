using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("move");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // State Transition - Idle
        if (!player.AnyMoveKeyPressed)
        {
            player.ChangeState(player.idleState);
        }

        //Input
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var movement = moveInput.normalized * player.Player.Stats.playerMoveSpeed;

        //Movement
        player.rb.MovePosition(player.rb.position + movement * Time.fixedDeltaTime);

        // Set idle Animation after move
        player.animator.Play("Run", 0);
        player.animator.Play("Run", 1);
        player.animator.Play("Run", 2);
        player.animator.Play("Run", 3);
        player.animator.Play("Run", 4);
        player.animator.Play("Run", 5);
        player.animator.Play("Run", 6);
        player.animator.Play("Run", 7);
        player.animator.Play("Run", 8);
        player.animator.Play("Run", 9);

        if (movement != Vector2.zero)
        {
            player.animator.SetFloat("Horizontal", movement.x);
            player.animator.SetFloat("Vertical", movement.y);
        }
        player.animator.SetFloat("Speed", movement.sqrMagnitude);

        if (player.animator.GetFloat("Vertical") >= 5)
        {
            GameObject.Find("Sword").GetComponent<SpriteRenderer>().sortingOrder = 1;
        } else
        {
            GameObject.Find("Sword").GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
