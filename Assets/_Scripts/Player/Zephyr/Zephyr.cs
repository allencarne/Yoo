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

    [Header("Parry Strike")]
    [SerializeField] GameObject parryStrikeShieldPrefab;
    [SerializeField] GameObject parryStrikePrefab;
    [SerializeField] float parryStrikeCoolDown;
    bool isParryStrikeTriggered = false;

    [Header("Lunge Strike")]
    [SerializeField] GameObject lungeStrikePrefab;
    [SerializeField] float lungeStrikeVelocity;
    [SerializeField] float lungeStrikeDuration;
    [SerializeField] float lungeStrikeCoolDown;

    private void OnEnable()
    {
        EnemyHitBox.OnPlayerParry += PlayerParry;
    }

    private void OnDisable()
    {
        EnemyHitBox.OnPlayerParry -= PlayerParry;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!canMobility && state == PlayerState.Mobility)
        {
            rb.velocity = angleToMouse.normalized * tempestChargeVelocity;
        }

        if (!canUtility && state == PlayerState.Utility)
        {
            rb.velocity = angleToMouse.normalized * lungeStrikeVelocity;
        }
    }

    #region Wind Slash

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

    #endregion

    #region Wind Slash 2

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

    #endregion

    #region Wind Slash 3

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

    #endregion

    #region Sweeping Gust

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

    #endregion

    #region Tempest Charge

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

            // Dust
            Instantiate(tempestChargePrefab, transform.position, aimer.rotation);

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

    #endregion

    #region Parry Strike

    protected override void PlayerDefensiveState()
    {
        if (canDefensive)
        {
            canDefensive = false;

            // Animate
            animator.Play("Sword Prepared Stance");
            animator.Play("Sword Prepared Stance", 1);

            // Disable Player Collider
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            // Instantiate Shield that has a collider
            Instantiate(parryStrikeShieldPrefab, transform.position, transform.rotation, transform);

            StartCoroutine(ParryStrikeShieldDuration());
            StartCoroutine(ParryStrikeCoolDown());
        }

        if (isParryStrikeTriggered)
        {
            isParryStrikeTriggered = false;

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

            StartCoroutine(ParryStrikeWindUpTime());
        }
    }

    IEnumerator ParryStrikeWindUpTime()
    {
        yield return new WaitForSeconds(.3f);

        Instantiate(parryStrikePrefab, transform.position, aimer.rotation);
    }

    IEnumerator ParryStrikeShieldDuration()
    {
        yield return new WaitForSeconds(1);

        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        state = PlayerState.Idle;
    }

    IEnumerator ParryStrikeCoolDown()
    {
        yield return new WaitForSeconds(parryStrikeCoolDown);
        canDefensive = true;
    }

    void PlayerParry()
    {
        isParryStrikeTriggered = true;
    }

    #endregion

    protected override void PlayerUtilityState()
    {
        if (canUtility)
        {
            canUtility = false;

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

            // Ignores collision with Enemy
            Physics2D.IgnoreLayerCollision(3, 6, true);

            Instantiate(lungeStrikePrefab, transform.position, aimer.rotation);

            StartCoroutine(LungeStrikeDuration());
            StartCoroutine(LungeStrikeCoolDown());
        }
    }

    IEnumerator LungeStrikeDuration()
    {
        yield return new WaitForSeconds(lungeStrikeDuration);

        rb.velocity = Vector2.zero;

        state = PlayerState.Idle;
    }

    IEnumerator LungeStrikeCoolDown()
    {
        yield return new WaitForSeconds(lungeStrikeCoolDown);

        canUtility = true;
    }

    protected override void PlayerUltimateState()
    {
        
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