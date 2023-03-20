using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float enemyCurrentSpeed;
    [SerializeField] protected float wanderRadius;
    [SerializeField] protected float aggroRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float resetRange;
    [SerializeField] protected float idleTime;
    [SerializeField] protected Vector2 newMoveDirection;

    bool canWander = false;

    // Components
    protected Transform target;
    protected Rigidbody2D enemyRB;
    protected Animator enemyAnimator;

    
    float spawnAnimationDuration = 1.8f;
    float hurtAnimationDuration = .8f;

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

    protected EnemyState enemyState = EnemyState.spawn;

    void Awake()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB= GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        enemyCurrentSpeed = enemySpeed;
    }

    protected virtual void Update()
    {
        Debug.Log(enemyState);

        switch (enemyState)
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
            enemyState = EnemyState.hurt;
        }
    }

    private void FixedUpdate()
    {
        if (newMoveDirection != Vector2.zero)
        {
            enemyRB.velocity = newMoveDirection * enemyCurrentSpeed;
        }
    }

    protected virtual void EnemySpawnState()
    {
        // Animate
        enemyAnimator.Play("Spawn");

        // Behaviour
        StartCoroutine(WaitForSpawn());
    }

    IEnumerator WaitForSpawn()
    {
        // Disable Collider and RigidBody

        yield return new WaitForSeconds(spawnAnimationDuration);

        // Enable Collider and RigidBody

        enemyState = EnemyState.idle;
    }

    public virtual void EnemyIdleState()
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
                    enemyState = EnemyState.wander;
                    canWander = true;
                    idleTime = 0;
                    break;
                case 1:
                    idleTime = 0;
                    break;
            }
        }

        // Transitions
        TransitionToChase();
        TransitionToAttack();
    }

    public void EnemyWanderState()
    {
        // Animate
        enemyAnimator.Play("Wander");
        enemyAnimator.SetFloat("Horizontal", newMoveDirection.x);
        enemyAnimator.SetFloat("Vertical", newMoveDirection.y);

        // Behaviour
        if (canWander)
        {
            // Prevents from wandering more than once
            canWander = false;

            // Get Random direction inside wander radius
            newMoveDirection = Random.insideUnitCircle * wanderRadius;

            // Movement code is in FixedUpdate
            StartCoroutine(WanderDelay());
        }

        // Transition
        TransitionToChase();
        TransitionToAttack();
    }

    IEnumerator WanderDelay()
    {
        yield return new WaitForSeconds(2);
        
        // Reset Wander Bool
        canWander = true;

        // Prevents Enemy from sliding after wander is finished
        enemyRB.velocity = new Vector2(0, 0);

        // Reset New Move Direction to 0 (Condition is with fixed update)
        newMoveDirection = Vector2.zero;

        // Transition to Idle
        enemyState = EnemyState.idle;
    }

    public void EnemyChaseState()
    {
        // Animate
        enemyAnimator.Play("Chase");
        enemyAnimator.SetFloat("Horizontal", target.position.x - enemyRB.position.x);
        enemyAnimator.SetFloat("Vertical", target.position.y - enemyRB.position.y);

        // Behaviour
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();

            enemyRB.MovePosition(enemyRB.position + direction * enemyCurrentSpeed * Time.fixedDeltaTime);
        }

        // Transitions
        TransitionToIdle();
        //TransitionToAttack();
    }

    public void EnemyResetState()
    {

    }

    public void EnemyAttackState()
    {

    }

    protected virtual void EnemyHurtState()
    {
        // Animate
        enemyAnimator.Play("Hurt");

        StartCoroutine(WaitForHurt());
    }

    IEnumerator WaitForHurt()
    {
        yield return new WaitForSeconds(hurtAnimationDuration);

        enemyState = EnemyState.idle;
    }

    public void EnemyDeathState()
    {
        
    }

    // Helper Methods
    public void TransitionToIdle()
    {
        // If target is outsite aggro range - Idle
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) >= aggroRange)
            {
                enemyState = EnemyState.idle;
            }
        }
    }

    public void TransitionToChase()
    {
        // If target is in aggro range - Chase
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= aggroRange)
            {
                enemyState = EnemyState.chase;
            }
        }
    }

    public void TransitionToAttack()
    {
        // If target is in attack range - Attack
        if (target != null)
        {
            if (Vector2.Distance(target.position, enemyRB.position) <= attackRange)
            {
                enemyState = EnemyState.attack;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, resetRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
