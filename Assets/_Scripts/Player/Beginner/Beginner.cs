using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginner : Player
{
    [Header("FireSlash")]
    [SerializeField] GameObject beginnerSwordAttack;
    [SerializeField] GameObject beginnerBowAttack;
    [SerializeField] float fireSlashCoolDown;
    [SerializeField] bool isFireSlashActive;


    protected override void Update()
    {
        base.Update();

        if (PlayerClassSelection.begginerWithSword)
        {
            Debug.Log("Sword Test");
        }
        if (PlayerClassSelection.begginerWithBow)
        {
            Debug.Log("Bow Test");
        }
        if (PlayerClassSelection.begginerWithStaff)
        {
            Debug.Log("Staff Test");
        }
        if (PlayerClassSelection.begginerWithDagger)
        {
            Debug.Log("Dagger Test");
        }
    }

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
        BasicAttackKeyPressed();
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
        movement = moveInput.normalized * playerManager.player_SO.movementSpeed;

        // Transitions
        NoMoveKeyPressed();
        BasicAttackKeyPressed();
    }

    protected override void PlayerBasicAttackState()
    {
        if (PlayerClassSelection.begginerWithSword)
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

                Instantiate(beginnerSwordAttack, transform.position, aimer.rotation);
            }
        }
        if (PlayerClassSelection.begginerWithBow)
        {
            if (canBasicAttack)
            {
                canBasicAttack = false;

                // Animation
                animator.Play("Bow Shoot");
                animator.Play("Bow Shoot", 2);

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

                var arrow = Instantiate(beginnerBowAttack,midAimer.position, midAimer.rotation);
                var arrowRB = arrow.GetComponent<Rigidbody2D>();

                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; // Set a distance from the camera
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                // Calculate the direction from the launcher to the mouse position
                Vector2 direction = (worldPosition - midAimer.position).normalized;

                arrowRB.AddForce(direction * 10, ForceMode2D.Impulse);

                //angleToMouse = angleToMouse.normalized * 10;
                //arrowRB.AddForce(angleToMouse, ForceMode2D.Impulse);
                //Destroy(arrow, .7f);


                // Instantiate the projectile at the launcher position
                //GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // Set the velocity of the projectile to move in the calculated direction
                //Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
                //projectileRb.velocity = direction * projectileSpeed;

            }
        }
        if (PlayerClassSelection.begginerWithStaff)
        {
            if (canBasicAttack)
            {
                canBasicAttack = false;

                // Animation
                animator.Play("Staff Swing Right");
                animator.Play("Staff Swing Right", 3);

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

                Instantiate(beginnerSwordAttack, transform.position, aimer.rotation);
            }
        }
        if (PlayerClassSelection.begginerWithDagger)
        {
            
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
