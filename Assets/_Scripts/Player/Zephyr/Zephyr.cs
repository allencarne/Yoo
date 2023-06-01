using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zephyr : Player
{
    [Header("WindSlash")]
    bool isWindSlashActive = false;

    [Header("Slicing Winds")]
    bool canSlicingWinds = false;

    //[Header("Gust Charge")]

    [Header("Parry Strike")]
    bool isCounterTriggered = false;

    //[Header("Heavy Blow")]

    //[Header("Engulf")]

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
            rb.velocity = angleToMouse.normalized * playerManager.player_SO.gustChargeVelocity;
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

            Instantiate(playerManager.player_SO.windSlashPrefab, transform.position, aimer.rotation);
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

            Instantiate(playerManager.player_SO.windSlash2Prefab, transform.position, aimer.rotation);
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

            Instantiate(playerManager.player_SO.windSlash3Prefab, transform.position, aimer.rotation);
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

            // Ignores collision with Enemy
            Physics2D.IgnoreLayerCollision(3, 6, true);

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

        Instantiate(playerManager.player_SO.slicingWindsPrefab, transform.position, aimer.rotation);
    }

    IEnumerator SlicingWindsDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.slicingWindsSlideDuration);

        // No Longer Ignores collision with Enemy
        Physics2D.IgnoreLayerCollision(3, 6, false);

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

    #region Gust Charge

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
            Instantiate(playerManager.player_SO.gustChargePrefab, transform.position, aimer.rotation);

            // Agility
            Agility(3f);

            StartCoroutine(UnpauseAimer(playerManager.player_SO.gustChargeDuration));
            StartCoroutine(GustChargeDuration());
            StartCoroutine(GustChargeCoolDown());
        }
    }

    IEnumerator GustChargeDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.gustChargeDuration);

        // No Longer Ignores collision with Enemy
        Physics2D.IgnoreLayerCollision(3, 6, false);

        rb.velocity = Vector2.zero;

        state = PlayerState.Idle;
    }

    IEnumerator GustChargeCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.gustChargeCoolDown);

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
            Instantiate(playerManager.player_SO.parryStrikeShieldPrefab, transform.position, transform.rotation, transform);

            StartCoroutine(ParryStrikeShieldDuration());
            StartCoroutine(ParryStrikeCoolDown());
        }

        if (isCounterTriggered)
        {
            isCounterTriggered = false;

            // Animation
            animator.Play("Sword Swing Right");
            animator.Play("Sword Swing Right", 1);

            // Heal
            float value = (int)(playerManager.player_SO.maxHealth * 25 / 100);
            RestoreHealth(value);

            AngleToMouse();

            SetAnimationDirection();

            StartCoroutine(HeavyBlowCastTime());
        }
    }

    IEnumerator ParryStrikeShieldDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.parryStrikeShieldDuration);

        gameObject.GetComponentInParent<CircleCollider2D>().enabled = true;

        state = PlayerState.Idle;
    }

    IEnumerator ParryStrikeCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.parryStrikeCoolDown);
        canDefensive = true;
    }

    IEnumerator HeavyBlowCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.heavyBlowCastTime);

        Instantiate(playerManager.player_SO.heavyBlowPrefab, transform.position, aimer.rotation);
    }

    void PlayerParry()
    {
        isCounterTriggered = true;
    }

    #endregion

    #region Whirling Slash

    protected override void PlayerUtilityState()
    {
        if (canUtility)
        {
            canUtility = false;

            // Animation
            animator.Play("Sword Prepared Stance");
            animator.Play("Sword Prepared Stance", 1);

            StartCoroutine(whirlingSlashDelay());
        }
    }

    IEnumerator whirlingSlashDelay()
    {
        yield return new WaitForSeconds(playerManager.player_SO.whirlingSlashDelay);

        animator.Play("Sword Swing Right");
        animator.Play("Sword Swing Right", 1);

        AngleToMouse();

        SetAnimationDirection();

        PauseAimer();

        StartCoroutine(UnpauseAimer(playerManager.player_SO.whirlingSlashDuration));
        StartCoroutine(WhirlingSlashCastTime());
        StartCoroutine(WhirlingSlashAnimationDuration());
        StartCoroutine(WhirlingSlashCoolDown());
    }

    IEnumerator WhirlingSlashCastTime()
    {
        yield return new WaitForSeconds(playerManager.player_SO.whirlingSlashCastTime);

        Instantiate(playerManager.player_SO.whirlingSlashPrefab, transform.position, aimer.rotation);
        //Instantiate(playerManager.player_SO.heavyBlowPrefab, transform.position, aimer.rotation);
    }

    IEnumerator WhirlingSlashAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.whirlingSlashDuration);

        state = PlayerState.Idle;
    }

    IEnumerator WhirlingSlashCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.whirlingSlashCoolDown);

        canUtility = true;
    }

    #endregion

    #region Zephyr's Fury

    protected override void PlayerUltimateState()
    {
        if (canUltimate)
        {
            canUltimate = false;

            animator.Play("Power-Up");
            animator.Play("Power-Up", 1);

            // Heal
            float value = (int)(playerManager.player_SO.maxHealth * 25 / 100);
            RestoreHealth(value);

            // Agility
            Agility(5f);

            var ult = Instantiate(playerManager.player_SO.zephyrsFuryPrefab, transform.position, transform.rotation, transform);
            Destroy(ult, playerManager.player_SO.zephyrsFuryDuration);

            StartCoroutine(ZephyrsFuryAnimationDuration());
            StartCoroutine(ZephyrsFuryCoolDown());
        }
    }

    IEnumerator ZephyrsFuryAnimationDuration()
    {
        yield return new WaitForSeconds(playerManager.player_SO.zephyrsFuryAnimationDuration);

        state = PlayerState.Idle;
    }

    IEnumerator ZephyrsFuryCoolDown()
    {
        yield return new WaitForSeconds(playerManager.player_SO.zephyrsFuryCoolDown);

        canUltimate = true;
    }

    #endregion

    protected override void UltimateKeyPressed()
    {
        if (Input.GetKey(keys.ultimateKey) && canUltimate)
        {
            if (playerManager.player_SO.fury >= playerManager.player_SO.maxFury)
            {
                state = PlayerState.Ultimate;
                LoseFury(playerManager.player_SO.maxFury);
            }
        }
    }

    IEnumerator UnpauseAimer(float time)
    {
        yield return new WaitForSeconds(time);

        AimIndicator.pauseDirection = false;
    }
}