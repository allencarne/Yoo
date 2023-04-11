using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : Player
{
    [Header("WindSlash")]
    [SerializeField] GameObject windSlashPrefab;
    [SerializeField] GameObject windSlash2Prefab;
    [SerializeField] GameObject windSlash3Prefab;
    [SerializeField] float windSlashDamage;
    [SerializeField] float windSlashCoolDown;
    [SerializeField] float windSlashKnockBackForce;
    bool isWindSlashActive = false;

    [Header("Sweeping Gust")]
    [SerializeField] GameObject sweepingGustPrefab;
    [SerializeField] float sweepingGustDamage;
    [SerializeField] float sweepingGustCoolDown;
    [SerializeField] float sweepingGustForce;
    [SerializeField] float sweepingGustPullForce;
    bool isSweepingGustActive = false;

    [Header("Sweeping Gust")]
    [SerializeField] GameObject tempestChargePrefab;
    [SerializeField] float tempestChargeVelocity;
    [SerializeField] float tempestChargeDuration;
    [SerializeField] float tempestChargeCoolDown;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!canMobility && state == PlayerState.Mobility)
        {
            rb.velocity = angleToMouse.normalized * tempestChargeVelocity;
        }
    }

    protected override void PlayerBasicAttackState()
    {
        // Begin Sword Swing Animation
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

            StartCoroutine(WindSlashDelay());
            StartCoroutine(BasicAttackAnimationDuration());
            StartCoroutine(BasicAttackCoolDown());
        }

        // Instantiate WindSlash Prefab
        if (isWindSlashActive)
        {
            isWindSlashActive = false;

            canSlide = true;

            Instantiate(windSlashPrefab, transform.position, aimer.rotation);
        }

        BasicAttack2KeyPressed();
    }

    IEnumerator WindSlashDelay()
    {
        yield return new WaitForSeconds(.3f);
        isWindSlashActive = true;
    }

    IEnumerator BasicAttackAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        if (state == PlayerState.BasicAttack)
        {
            canBasicAttack2 = false;
            state = PlayerState.Idle;
        }
    }

    IEnumerator BasicAttackCoolDown()
    {
        yield return new WaitForSeconds(windSlashCoolDown);

        canBasicAttack = true;
    }

    protected override void PlayerBasicAttack2State()
    {
        if (canBasicAttack2)
        {
            canBasicAttack2 = false;

            // Animate
            animator.Play("Sword Swing Left");
            animator.Play("Sword Swing Left", 1);

            // Calculates angle from mouse position and player position
            angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // Set Attack Animation Depending on Mouse Position
            animator.SetFloat("Aim Horizontal", angleToMouse.x);
            animator.SetFloat("Aim Vertical", angleToMouse.y);
            // Set Idle to last attack position
            animator.SetFloat("Horizontal", angleToMouse.x);
            animator.SetFloat("Vertical", angleToMouse.y);

            StartCoroutine(WindSlashDelay());
            StartCoroutine(BasicAttack2AnimationDuration());
        }

        // Instantiate WindSlash Prefab
        if (isWindSlashActive)
        {
            isWindSlashActive = false;

            canSlide = true;

            Instantiate(windSlash2Prefab, transform.position, aimer.rotation);
        }

        BasicAttack3KeyPressed();
    }

    IEnumerator BasicAttack2AnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        if (state == PlayerState.BasicAttack2)
        {
            canBasicAttack3 = false;
            state = PlayerState.Idle;
        }
    }

    protected override void PlayerBasicAttack3State()
    {
        if (canBasicAttack3)
        {
            canBasicAttack3 = false;

            // Animate
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

            StartCoroutine(WindSlashDelay());
            StartCoroutine(BasicAttack3AnimationDuration());
        }

        // Instantiate WindSlash Prefab
        if (isWindSlashActive)
        {
            isWindSlashActive = false;

            canSlide = true;

            Instantiate(windSlash3Prefab, transform.position, aimer.rotation);
        }
    }

    IEnumerator BasicAttack3AnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        if (state == PlayerState.BasicAttack3)
        {
            state = PlayerState.Idle;
        }
    }

    protected override void PlayerAbilityState()
    {
        if (canAbility)
        {
            canAbility = false;

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

            StartCoroutine(SweepingGustDelay());
            StartCoroutine(SweepingGustAnimationDuration());
            StartCoroutine(SweepingGustCoolDown());
        }

        if (isSweepingGustActive)
        {
            isSweepingGustActive = false;

            GameObject gust = Instantiate(sweepingGustPrefab, transform.position, Quaternion.Inverse(aimer.rotation));
            Rigidbody2D gustRB = gust.GetComponent<Rigidbody2D>();
            gustRB.AddForce(angleToMouse.normalized * sweepingGustForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator SweepingGustDelay()
    {
        yield return new WaitForSeconds(.3f);
        isSweepingGustActive = true;
    }

    IEnumerator SweepingGustAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);
        state = PlayerState.Idle;
    }

    IEnumerator SweepingGustCoolDown()
    {
        yield return new WaitForSeconds(sweepingGustCoolDown);
        canAbility = true;
    }

    protected override void PlayerMobilityState()
    {
        if (canMobility)
        {
            canMobility = false;

            // Animation
            animator.Play("Run");
            animator.Play("Run", 1);

            // Calculates angle from mouse position and player position
            angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // Set Attack Animation Depending on Mouse Position
            animator.SetFloat("Aim Horizontal", angleToMouse.x);
            animator.SetFloat("Aim Vertical", angleToMouse.y);
            // Set Idle to last attack position
            animator.SetFloat("Horizontal", angleToMouse.x);
            animator.SetFloat("Vertical", angleToMouse.y);

            StartCoroutine(TempestChargeDuration());
            StartCoroutine(TempestChargeCoolDown());
        }
    }

    IEnumerator TempestChargeDuration()
    {
        yield return new WaitForSeconds(tempestChargeDuration);

        rb.velocity = Vector2.zero;

        state = PlayerState.Idle;
    }

    IEnumerator TempestChargeCoolDown()
    {
        yield return new WaitForSeconds(tempestChargeCoolDown);

        canMobility = true;
    }

    // Animation Events

    public void AE_WindSlash2()
    {
        if (state == PlayerState.BasicAttack)
        {
            canBasicAttack2 = true;
        }
    }

    public void AE_WindSlash3()
    {
        canBasicAttack3 = true;
    }
}