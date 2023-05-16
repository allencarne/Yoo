using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : Player
{
    [Header("WindSlash")]
    [SerializeField] GameObject windSlashPrefab;
    [SerializeField] GameObject windSlash2Prefab;
    [SerializeField] GameObject windSlash3Prefab;
    bool isWindSlashActive = false;

    [Header("Slicing Winds")]
    [SerializeField] GameObject slicingWindsPrefab;
    bool canSlicingWinds = false;

    [Header("Tempest Charge")]
    [SerializeField] GameObject tempestChargePrefab;

    [Header("Parry Strike")]
    [SerializeField] GameObject parryStrikeShieldPrefab;
    [SerializeField] GameObject parryStrikePrefab;
    bool isParryStrikeTriggered = false;

    [Header("Heavy Blow")]
    [SerializeField] GameObject heavyBlowPrefab;
    //[SerializeField] float heavyBlowCoolDown;

    [Header("Engulf")]
    [SerializeField] GameObject engulfPrefab;
    //[SerializeField] float engulfVelocity;
    //[SerializeField] float engulfAnimationDuration;
    //[SerializeField] float engulfCoolDown;

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

        if (canSlicingWinds && state == PlayerState.Ability)
        {
            rb.velocity = angleToMouse.normalized * playerManager.player_SO.slicingWindsSlideForce;
        }

        if (!canMobility && state == PlayerState.Mobility)
        {
            rb.velocity = angleToMouse.normalized * playerManager.player_SO.tempestChargeVelocity;
        }

        if (canSlide)
        {
            canSlide = false;

            SlideForward(playerManager.player_SO.windSlashSlideForce);
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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer(playerManager.player_SO.windSlashDuration));
            StartCoroutine(WindSlashCastTime());
            StartCoroutine(TransitionToWinsSlash2());
            StartCoroutine(WindSlashAnimationDuration());
            StartCoroutine(WindSlashCoolDown());
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

    IEnumerator WindSlashCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.windSlashCastTime);

        isWindSlashActive = true;
    }

    IEnumerator TransitionToWinsSlash2()
    {
        yield return new WaitForSeconds(.5f);

        canBasicAttack2 = true;
    }

    IEnumerator WindSlashAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.windSlashDuration);

        if (state == PlayerState.BasicAttack)
        {
            canBasicAttack2 = false;
            state = PlayerState.Idle;
        }
    }

    IEnumerator WindSlashCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.windSlashCoolDown);

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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer(playerManager.player_SO.windSlashDuration));
            StartCoroutine(WindSlashCastTime());
            StartCoroutine(TransitionToWinsSlash3());
            StartCoroutine(WindSlash2AnimationDuration());
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

    IEnumerator TransitionToWinsSlash3()
    {
        yield return new WaitForSeconds(.5f);

        canBasicAttack3 = true;
    }

    IEnumerator WindSlash2AnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.windSlashDuration);

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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer(playerManager.player_SO.windSlashDuration));
            StartCoroutine(WindSlashCastTime());
            StartCoroutine(WindSlash3AnimationDuration());
        }

        // Instantiate WindSlash Prefab
        if (isWindSlashActive)
        {
            isWindSlashActive = false;

            canSlide = true;

            Instantiate(windSlash3Prefab, transform.position, aimer.rotation);
        }
    }

    IEnumerator WindSlash3AnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.windSlashDuration);

        if (state == PlayerState.BasicAttack3)
        {
            state = PlayerState.Idle;
        }
    }

    #endregion

    #region Slicing Winds

    protected override void PlayerAbilityState()
    {
        if (canAbility)
        {
            canAbility = false;

            // Animation
            animator.Play("Sword Swing Right");
            animator.Play("Sword Swing Right", 1);

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            canSlicingWinds = true;

            StartCoroutine(UnpauseAimer(playerManager.player_SO.slicingWindsDuration));
            StartCoroutine(SlicingWindsCastTime());
            StartCoroutine(SlicingWindsDuration());
            StartCoroutine(SlicingWindsAnimationDuration());
            StartCoroutine(SlicingWindsCoolDown());
        }
    }

    IEnumerator SlicingWindsCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.slicingWindsCastTime);

        Instantiate(slicingWindsPrefab, transform.position, aimer.rotation);
    }

    IEnumerator SlicingWindsDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.slicingWindsSlideDuration);

        rb.velocity = Vector2.zero;

        canSlicingWinds = false;
    }

    IEnumerator SlicingWindsAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.slicingWindsDuration);

        state = PlayerState.Idle;
    }

    IEnumerator SlicingWindsCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.slicingWindsCoolDown);

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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            // Ignores collision with Enemy
            Physics2D.IgnoreLayerCollision(3, 6, true);

            // Dust
            Instantiate(tempestChargePrefab, transform.position, aimer.rotation);

            StartCoroutine(UnpauseAimer(playerManager.player_SO.tempestChargeDuration));
            StartCoroutine(TempestChargeDuration());
            StartCoroutine(TempestChargeCoolDown());
        }
    }

    IEnumerator TempestChargeDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.tempestChargeDuration);

        // No Longer Ignores collision with Enemy
        Physics2D.IgnoreLayerCollision(3, 6, false);

        rb.velocity = Vector2.zero;

        state = PlayerState.Idle;
    }

    IEnumerator TempestChargeCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.tempestChargeCoolDown);

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
            gameObject.GetComponentInParent<CircleCollider2D>().enabled = false;

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

            AngleToMouse();

            SetAnimationDirection();

            StartCoroutine(ParryStrikeCastTime());
        }
    }

    IEnumerator ParryStrikeShieldDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.parryStrikeDuration);

        gameObject.GetComponentInParent<CircleCollider2D>().enabled = true;

        state = PlayerState.Idle;
    }

    IEnumerator ParryStrikeCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.parryStrikeCoolDown);
        canDefensive = true;
    }

    IEnumerator ParryStrikeCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.parryStrikeCastTime);

        Instantiate(parryStrikePrefab, transform.position, aimer.rotation);
    }

    void PlayerParry()
    {
        isParryStrikeTriggered = true;
    }

    #endregion

    #region Heavy Blow

    protected override void PlayerUtilityState()
    {
        if (canUtility)
        {
            canUtility = false;

            // Animation
            animator.Play("Sword Prepared Stance");
            animator.Play("Sword Prepared Stance", 1);

            StartCoroutine(HeavyBlowDelay());
        }
    }

    IEnumerator HeavyBlowDelay()
    {
        yield return new WaitForSeconds(.5f);

        animator.Play("Sword Swing Right");
        animator.Play("Sword Swing Right", 1);

        AngleToMouse();

        SetAnimationDirection();

        PauseAimer();

        StartCoroutine(UnpauseAimer(playerManager.player_SO.heavyBlowDuration));
        StartCoroutine(HeavyBlowCastTime());
        StartCoroutine(HeavyBlowAnimationDuration());
        StartCoroutine(HeavyBlowCoolDown());
    }

    IEnumerator HeavyBlowCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.heavyBlowCastTime);

        Instantiate(heavyBlowPrefab, transform.position, aimer.rotation);
    }

    IEnumerator HeavyBlowAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.heavyBlowDuration);

        state = PlayerState.Idle;
    }

    IEnumerator HeavyBlowCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.heavyBlowCoolDown);

        canUtility = true;
    }

    #endregion

    #region Engulf

    protected override void PlayerUltimateState()
    {
        if (canUltimate)
        {
            canUltimate = false;

            animator.Play("Power-Up");
            animator.Play("Power-Up", 1);

            Instantiate(engulfPrefab, transform.position, transform.rotation);

            StartCoroutine(EngulfAnimationDuration());
            StartCoroutine(EngulfCoolDown());
        }
    }

    IEnumerator EngulfAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.engulfDuration);

        state = PlayerState.Idle;
    }

    IEnumerator EngulfCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.engulfCoolDown);

        canUltimate = true;
    }

    #endregion

    IEnumerator UnpauseAimer(float time)
    {
        yield return new WaitForSeconds(time);

        AimIndicator.pauseDirection = false;
    }
}