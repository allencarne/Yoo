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

    [Header("Slicing Winds")]
    [SerializeField] GameObject slicingWindsPrefab;
    [SerializeField] float slicingWindsCoolDown;
    [SerializeField] float slicingWindsVelocity;
    [SerializeField] float slicingWindsDuration;
    bool canSlicingWinds = false;

    [Header("Tempest Charge")]
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
    bool canLungeStrike;

    [Header("Engulf")]
    [SerializeField] GameObject engulfPrefab;
    [SerializeField] float engulfVelocity;
    [SerializeField] float engulfAnimationDuration;
    [SerializeField] float engulfCoolDown;

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
            rb.velocity = angleToMouse.normalized * slicingWindsVelocity;
        }

        if (!canMobility && state == PlayerState.Mobility)
        {
            rb.velocity = angleToMouse.normalized * tempestChargeVelocity;
        }

        if (canLungeStrike && state == PlayerState.Utility)
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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer());
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
        yield return new WaitForSeconds(.3f);

        isWindSlashActive = true;
    }

    IEnumerator TransitionToWinsSlash2()
    {
        yield return new WaitForSeconds(.5f);

        canBasicAttack2 = true;
    }

    IEnumerator WindSlashAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        if (state == PlayerState.BasicAttack)
        {
            canBasicAttack2 = false;
            state = PlayerState.Idle;
        }
    }

    IEnumerator WindSlashCoolDown()
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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer());
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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            StartCoroutine(UnpauseAimer());
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

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            canSlicingWinds = true;

            StartCoroutine(UnpauseAimer());
            StartCoroutine(SlicingWindsCastTime());
            StartCoroutine(SlicingWindsDuration());
            StartCoroutine(SlicingWindsAnimationDuration());
            StartCoroutine(SlicingWindsCoolDown());
        }
    }

    IEnumerator SlicingWindsCastTime()
    {
        yield return new WaitForSeconds(.3f);

        Instantiate(slicingWindsPrefab, transform.position, aimer.rotation);
    }

    IEnumerator SlicingWindsDuration()
    {
        yield return new WaitForSeconds(slicingWindsDuration);

        rb.velocity = Vector2.zero;

        canSlicingWinds = false;
    }

    IEnumerator SlicingWindsAnimationDuration()
    {
        yield return new WaitForSeconds(.7f);

        state = PlayerState.Idle;
    }

    IEnumerator SlicingWindsCoolDown()
    {
        yield return new WaitForSeconds(slicingWindsCoolDown);

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

            StartCoroutine(UnpauseAimer());
            StartCoroutine(TempestChargeDuration());
            StartCoroutine(TempestChargeCoolDown());
        }
    }

    IEnumerator TempestChargeDuration()
    {
        yield return new WaitForSeconds(tempestChargeDuration);

        // Ignores collision with Enemy
        Physics2D.IgnoreLayerCollision(3, 6, false);

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

            AngleToMouse();

            SetAnimationDirection();

            StartCoroutine(ParryStrikeCastTime());
        }
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

    IEnumerator ParryStrikeCastTime()
    {
        yield return new WaitForSeconds(.3f);

        Instantiate(parryStrikePrefab, transform.position, aimer.rotation);
    }

    void PlayerParry()
    {
        isParryStrikeTriggered = true;
    }

    #endregion

    #region Lunge Strike

    protected override void PlayerUtilityState()
    {
        if (canUtility)
        {
            canUtility = false;

            // Animation
            animator.Play("Sword Swing Right");
            animator.Play("Sword Swing Right", 1);

            AngleToMouse();

            SetAnimationDirection();

            PauseAimer();

            // Ignores collision with Enemy
            Physics2D.IgnoreLayerCollision(3, 6, true);

            StartCoroutine(UnpauseAimer());
            StartCoroutine(LungeStrikeCastTime());
            StartCoroutine(LungeStrikeDuration());
            StartCoroutine(LungeStrikeCoolDown());
        }
    }

    IEnumerator LungeStrikeCastTime()
    {
        yield return new WaitForSeconds(.3f);

        canLungeStrike = true;

        Instantiate(lungeStrikePrefab, transform.position, aimer.rotation);
    }

    IEnumerator LungeStrikeDuration()
    {
        yield return new WaitForSeconds(lungeStrikeDuration);

        rb.velocity = Vector2.zero;

        canLungeStrike = false;

        state = PlayerState.Idle;
    }

    IEnumerator LungeStrikeCoolDown()
    {
        yield return new WaitForSeconds(lungeStrikeCoolDown);

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
        yield return new WaitForSeconds(engulfAnimationDuration);

        state = PlayerState.Idle;
    }

    IEnumerator EngulfCoolDown()
    {
        yield return new WaitForSeconds(engulfCoolDown);

        canUltimate = true;
    }

    #endregion

    //

    IEnumerator UnpauseAimer()
    {
        yield return new WaitForSeconds(.3f);

        AimIndicator.pauseDirection = false;
    }
}