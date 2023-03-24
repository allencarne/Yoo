using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator enemyAnimator;
    [SerializeField] Rigidbody2D enemyRB;
    Transform target;

    [Header("Variables")]
    [SerializeField] float enemyCurrentSpeed;
    [SerializeField] float aggroRange;
    [SerializeField] float resetRange;
    [SerializeField] float wanderRange;
    [SerializeField] float attackRange;
    float idleTime;
    Vector2 startingPosition;
    Vector2 resetDirection;
    Vector2 wanderDirection;

    bool canWander = false;
    bool isEnemyHurt = false;

    enum EnemyState
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

    EnemyState state = EnemyState.spawn;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        startingPosition = transform.position;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        Debug.Log(state);

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
                EnemyHurtState();
                break;
            case EnemyState.death:
                EnemyDeathState();
                break;
        }

        // Testing
        if (Input.GetKeyDown(KeyCode.X))
        {
            isEnemyHurt = false;
            state = EnemyState.hurt;
        }
    }

    private void FixedUpdate()
    {
        // Chase State Movement
        if (state == EnemyState.chase && target != null)
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();

            enemyRB.MovePosition(enemyRB.position + direction * enemyCurrentSpeed * Time.deltaTime);
        }

        // Reset State Movement
        if (state == EnemyState.reset)
        {
            resetDirection = startingPosition - enemyRB.position;
            resetDirection.Normalize();

            enemyRB.MovePosition(enemyRB.position + resetDirection * enemyCurrentSpeed * Time.deltaTime);
        }

        // Wander State Movement
        if (wanderDirection != Vector2.zero)
        {
            wanderDirection.Normalize();
            enemyRB.MovePosition(enemyRB.position + wanderDirection * enemyCurrentSpeed * Time.deltaTime);
        }
    }

    public void EnemySpawnState()
    {
        // Animate
        enemyAnimator.Play("Spawn");
    }

    public void EnemyIdleState()
    {
        // Animate
        enemyAnimator.Play("Idle");

        // Behaviour
        idleTime += 1 * Time.deltaTime;

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
                state = EnemyState.chase;
            }
        }
    }

    public void EnemyWanderState()
    {
        enemyAnimator.Play("Wander");
        enemyAnimator.SetFloat("Horizontal", wanderDirection.x);
        enemyAnimator.SetFloat("Vertical", wanderDirection.y);

        if (canWander)
        {
            canWander = false;

            wanderDirection = Random.insideUnitCircle * wanderRange;

            StartCoroutine(WanderDelay());
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
        enemyAnimator.SetFloat("Horizontal", target.position.x - enemyRB.position.x);
        enemyAnimator.SetFloat("Vertical", target.position.y - enemyRB.position.y);

        // If Target is outside reset range - Reset
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) >= resetRange)
            {
                state = EnemyState.reset;
            }
        }
    }

    public void EnemyAttackState()
    {

    }

    public void EnemyResetState()
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

    public void EnemyHurtState()
    {
        if (!isEnemyHurt)
        {
            isEnemyHurt= true;
            enemyAnimator.Play("Hurt", -1, 0f);
        }
    }

    public void EnemyDeathState()
    {

    }

    // Helper Methods

    public void AE_Spawn()
    {
        state = EnemyState.idle;
        this.GetComponent<CircleCollider2D>().enabled = true;
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
}
