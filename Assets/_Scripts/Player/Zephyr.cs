using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : Player
{
    [Header("Components")]
    [SerializeField] GameObject windSlashPrefab;

    [Header("Variables")]
    [SerializeField] float windSlashCoolDown;
    [SerializeField] float windSlashAttackRange;
    [SerializeField] float windSlashSlideForce;
    [SerializeField] bool isWindSlashActive = false;
    bool canSlide = false;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (canSlide)
        {
            canSlide = false;
            Debug.Log("Test");
            SlideForward();
        }
    }

    protected override void PlayerBasicAttackState()
    {
        if (canBasicAttack)
        {
            canBasicAttack = false;

            // Animation
            animator.Play("Sword Swing Right");
            animator.Play("Sword Swing Right", 1);

            // Calculates angle from mouse position and player position
            angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // Set Attack Animation Depending on Mouse Position
            animator.SetFloat("Aim Horizontal", angleToMouse.x);
            animator.SetFloat("Aim Vertical", angleToMouse.y);
            // Set Idle to last attack position
            animator.SetFloat("Horizontal", angleToMouse.x);
            animator.SetFloat("Vertical", angleToMouse.y);

            StartCoroutine(BasicAttackAnimationDuration());
            StartCoroutine(BasicAttackCoolDown());
        }

        if (isWindSlashActive)
        {
            isWindSlashActive = false;

            canSlide = true;

            Instantiate(windSlashPrefab, transform.position, aimer.rotation);
        }
    }

    IEnumerator BasicAttackAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        state = PlayerState.Idle;
    }

    IEnumerator BasicAttackCoolDown()
    {
        yield return new WaitForSeconds(windSlashCoolDown);

        canBasicAttack = true;
    }

    public void AE_WindSlash()
    {
        isWindSlashActive = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, windSlashAttackRange);
    }

    protected virtual void SlideForward()
    {
        // Calculates the difference between the mouse position and player position
        angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // If Mouse is outside attack range - Slide
        if (Vector3.Distance(rb.position, cam.ScreenToWorldPoint(Input.mousePosition)) > windSlashAttackRange)
        {
            // Normalize movement vector and times it by attack move distance
            angleToMouse = angleToMouse.normalized * windSlashSlideForce;

            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * moveSpeed * Time.deltaTime);
        }

        // If Movement key is held while attacking - Slide
        if (Input.GetKey(upKey) || Input.GetKey(leftKey) || Input.GetKey(downKey) || Input.GetKey(rightKey))
        {
            // Normalize movement vector and times it by attack move distance
            angleToMouse = angleToMouse.normalized * windSlashSlideForce;

            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * moveSpeed * Time.deltaTime);
        }
    }
}