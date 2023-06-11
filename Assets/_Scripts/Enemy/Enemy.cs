using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float enemyHealth;
    public float enemyMaxHealth;
    [SerializeField] float enemyMoveSpeed;
    [SerializeField] float enemyMaxMoveSpeed;
    public float expAmount;

    [Header("Components")]
    StatusEffects statusEffects;
    [SerializeField] protected GameObject enemyUI;
    [SerializeField] EnemyHealthBar enemyHealthbar;
    [SerializeField] protected Animator enemyAnimator;
    [SerializeField] protected Transform enemyAimer;
    [SerializeField] protected Rigidbody2D enemyRB;
    [SerializeField] GameObject floatingTextDamage;
    [SerializeField] GameObject floatingTextHeal;
    protected Transform target;

    [Header("Variables")]
    [SerializeField] float aggroRange;
    [SerializeField] float resetRange;
    [SerializeField] float wanderRange;
    [SerializeField] float attackRange;
    protected float idleTime;
    protected Vector2 startingPosition;
    Vector2 resetDirection;
    Vector2 wanderDirection;
    float damage;

    protected bool canAttack = true;
    bool canWander = false;
    protected bool isEnemyHurt = false;
    bool inCombat = false;

    public static event System.Action OnEnemyHurt;

    protected enum EnemyState
    {
        spawn,
        idle,
        wander,
        chase,
        attack,
        reset,
        hurt,
        death
    }

    protected EnemyState state = EnemyState.spawn;

    private void Awake()
    {
        statusEffects = GetComponent<StatusEffects>();
    }

    private void Start()
    {
        // Set
        enemyHealth = enemyMaxHealth;
        enemyMoveSpeed = enemyMaxMoveSpeed;

        // Records the starting position for Reset State
        startingPosition = transform.position;

        // Prevents enemy from being attacked while spawning
        this.GetComponent<CircleCollider2D>().enabled = false;

        enemyUI.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.spawn:
                EnemySpawnState();
                break;
            case EnemyState.idle:
                EnemyIdleState();
                break;
            case EnemyState.wander:
                EnemyWanderState();
                break;
            case EnemyState.chase:
                EnemyChaseState();
                break;
            case EnemyState.attack:
                EnemyAttackState();
                break;
            case EnemyState.reset:
                EnemyResetState();
                break;
            case EnemyState.hurt:
                EnemyHurtState(damage);
                break;
            case EnemyState.death:
                EnemyDeathState();
                break;
        }

        // If target is outside of reset range - Reset (Also drops target and incombat is false)
        if (target)
        {
            if (Vector2.Distance(target.position, enemyRB.position) >= resetRange)
            {
                target = null;
                inCombat = false;
                state = EnemyState.reset;
            }
        }

        if (target == null && inCombat)
        {
            inCombat = false;
            state = EnemyState.reset;
        }

        EnemyAimer();
    }

    private void FixedUpdate()
    {
        // Chase State Movement
        if (state == EnemyState.chase && target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) >= attackRange)
            {
                Vector2 direction = target.position - transform.position;
                direction.Normalize();

                enemyRB.MovePosition(enemyRB.position + direction * enemyMoveSpeed * Time.deltaTime);
            }
        }

        // Reset State Movement
        if (state == EnemyState.reset)
        {
            resetDirection = startingPosition - enemyRB.position;
            resetDirection.Normalize();

            enemyRB.MovePosition(enemyRB.position + resetDirection * enemyMoveSpeed * Time.deltaTime);
        }

        // Wander State Movement
        if (wanderDirection != Vector2.zero)
        {
            wanderDirection.Normalize();
            enemyRB.MovePosition(enemyRB.position + wanderDirection * enemyMoveSpeed * Time.deltaTime);
        }
    }

    #region Enemy States
    public void EnemySpawnState()
    {
        // Animate
        enemyAnimator.Play("Spawn");
    }

    protected virtual void EnemyIdleState()
    {
        enemyUI.SetActive(true);

        // Enable Collider
        this.GetComponent<CircleCollider2D>().enabled = true;

        // Animate
        enemyAnimator.Play("Idle");

        // Prevents enemy from sliding after being attacked
        //enemyRB.velocity = Vector2.zero;

        // Increase idle time every second while in idle state
        idleTime += 1 * Time.deltaTime;

        // If idletime is over X, 50% chance of Wandering or resetting idle time
        if (idleTime >= 5)
        {
            int change = Random.Range(0, 2);
            switch (change)
            {
                case 0:
                    state = EnemyState.wander;
                    canWander = true;
                    idleTime = 0;
                    break;
                case 1:
                    idleTime = 0;
                    break;
            }
        }

        // If Target is inside aggro range - Chase
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= aggroRange)
            {
                idleTime = 0;
                state = EnemyState.chase;
            }
        }

        // If Target is inside reset range and in combat - Chase
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= resetRange && inCombat)
            {
                idleTime = 0;
                state = EnemyState.chase;
            }
        }
    }

    public void EnemyWanderState()
    {
        // Animate
        enemyAnimator.Play("Wander");
        enemyAnimator.SetFloat("Horizontal", wanderDirection.x);
        enemyAnimator.SetFloat("Vertical", wanderDirection.y);

        // Get a random wander direction
        if (canWander)
        {
            canWander = false;

            wanderDirection = Random.insideUnitCircle * wanderRange;

            StartCoroutine(WanderDelay());
        }

        // If Target is inside aggro range - Chase
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= aggroRange)
            {
                wanderDirection = Vector2.zero;
                state = EnemyState.chase;
            }
        }
    }

    IEnumerator WanderDelay()
    {
        yield return new WaitForSeconds(2);

        // Reset Wander Bool
        canWander = true;

        // Reset New Move Direction to 0 (Condition is with fixed update)
        wanderDirection = Vector2.zero;

        // Transition to Idle
        state = EnemyState.idle;

    }

    public void EnemyChaseState()
    {
        // Animation
        enemyAnimator.Play("Chase");

        // In Combat (Allows enemy to continue chasing after being attacked)
        inCombat = true;

        // If a target is found, Chase towards target - Else, Reset State
        if (target)
        {
            enemyAnimator.SetFloat("Horizontal", target.position.x - enemyRB.position.x);
            enemyAnimator.SetFloat("Vertical", target.position.y - enemyRB.position.y);
        } else
        {
            state = EnemyState.reset;
        }

        // If Target is outside reset range - Reset
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) >= resetRange)
            {
                state = EnemyState.reset;
            }
        }

        // If target is in attack range - Attack
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= attackRange && canAttack)
            {
                state = EnemyState.attack;
            }
        }
    }

    protected virtual void EnemyAttackState()
    {
        enemyRB.velocity = Vector2.zero;
    }

    protected virtual void EnemyResetState()
    {
        // Animation
        enemyAnimator.Play("Wander");
        enemyAnimator.SetFloat("Horizontal", resetDirection.x);
        enemyAnimator.SetFloat("Vertical", resetDirection.y);

        // Transition
        if (Vector2.Distance(startingPosition, enemyRB.position) <= 1)
        {
            state = EnemyState.idle;
        }
    }

    protected virtual void EnemyHurtState(float damage)
    {
        // Allows enemy to be pushed around
        //enemyRB.isKinematic = false;

        // Animate and If enemy telegraph is spawned, Destroy it
        if (isEnemyHurt)
        {
            isEnemyHurt = false;
            enemyAnimator.Play("Hurt", -1, 0f);
            if (target)
            {
                enemyAnimator.SetFloat("Horizontal", enemyRB.position.x - target.position.x);
                enemyAnimator.SetFloat("Vertical", enemyRB.position.y - target.position.y);
            }
            OnEnemyHurt?.Invoke();
        }

        // If enemy is wandering when entering hurt state - stops the wander movement
        wanderDirection = Vector2.zero;

        // Transition to Death State
        if (enemyHealth <= 0)
        {
            state = EnemyState.death;

            this.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void EnemyDeathState()
    {
        // Animate
        enemyAnimator.Play("Death");

        // Destroy after delay
        Destroy(gameObject, .7f);
    }
    #endregion

    #region Crowd Control

    public void KnockBack(Vector3 aPosition, Vector3 bPosition, Rigidbody2D opponentRB, float knockBackAmount)
    {
        statusEffects.knockBackIcon.SetActive(true);

        // Knock Back
        Vector2 direction = (aPosition - bPosition).normalized;
        opponentRB.velocity = direction * knockBackAmount;

        StartCoroutine(KnockBackDuration(opponentRB));
    }

    IEnumerator KnockBackDuration(Rigidbody2D opponentRB)
    {
        yield return new WaitForSeconds(.3f);

        opponentRB.velocity = Vector2.zero;

        statusEffects.knockBackIcon.SetActive(false);
    }

    public void Stun(float duration)
    {
        statusEffects.stunIcon.SetActive(true);

        enemyAnimator.speed = 0;

        StartCoroutine(StunDuration(duration));
    }

    IEnumerator StunDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        enemyAnimator.speed = 1;

        statusEffects.stunIcon.SetActive(false);
    }

    #endregion

    #region Debuffs

    public void Vulnerability()
    {
        statusEffects.vulnerabilityIcon.SetActive(true);

        // Reduce Max Health by a percentage
        float value = (int)(enemyMaxHealth * 25 / 100);

        enemyMaxHealth -= value;

        StartCoroutine(VulnerabilityDuration(value));
    }

    IEnumerator VulnerabilityDuration(float value)
    {
        yield return new WaitForSeconds(3f);

        // Set Max Health back to Original Percentage
        enemyMaxHealth += value;

        statusEffects.vulnerabilityIcon.SetActive(false);
    }

    #endregion

    #region Animation Events
    public void AE_Spawn()
    {
        // Set State
        state = EnemyState.idle;
    }

    public void AE_Hurt()
    {
        // Set State
        state = EnemyState.idle;
    }

    public void AE_Attack()
    {
        // Allows enemy to pushed around
        //enemyRB.isKinematic = false;

        // Set State
        state = EnemyState.idle;
    }
    #endregion

    #region Helper Methods
    public void TakeDamage(float damage)
    {
        // Enemy hurt bool - Necessary so hurt state only runs once
        isEnemyHurt = true;

        // Set State
        state = EnemyState.hurt;

        // Floating Text
        ShowDamage(damage.ToString());

        // Reduce Health
        enemyHealth -= damage;

        // Healthbar Lerp
        enemyHealthbar.lerpTimer = 0f;
    }

    void ShowDamage(string text)
    {
        if (floatingTextDamage)
        {
            GameObject prefab = Instantiate(floatingTextDamage, transform.position, Quaternion.identity, transform);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    void RestoreHealth(float healAmount)
    {
        ShowHeal(healAmount.ToString());
        enemyHealth += healAmount;
        enemyHealthbar.lerpTimer = 0f;
    }

    void ShowHeal(string text)
    {
        if (floatingTextHeal)
        {
            GameObject prefab = Instantiate(floatingTextHeal, transform.position, Quaternion.identity, transform);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public void EnemyAimer()
    {
        // Aim at target direction
        if (target)
        {
            enemyAimer.up = enemyAimer.transform.position - target.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, resetRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.transform;
        }
    }
    #endregion
}
