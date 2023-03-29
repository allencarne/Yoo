using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float maxHealth;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;

    [Header("Components")]
    [SerializeField] HealthBar healthbar;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform aimer;
    protected Camera cam;

    [Header("Variables")]
    [SerializeField] protected float basicAttackSlideForce;
    [SerializeField] protected float basicAttackRange;
    protected Vector2 angleToMouse;
    protected bool canSlide = false;
    Vector2 movement;
    bool isPlayerHurt = false;
    bool isPlayerDead = false;
    float damage;

    protected bool canBasicAttack = true;

    [Header("Keys")]
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode basicAttackKey;
    [SerializeField] KeyCode abilityKey;
    [SerializeField] KeyCode mobilityKey;
    [SerializeField] KeyCode defensiveKey;
    [SerializeField] KeyCode utilityKey;
    [SerializeField] KeyCode ultimateKey;

    protected enum PlayerState
    {
        Spawn,
        Idle,
        Run,
        Hurt,
        Death,
        BasicAttack,
        BasicAttack2,
        BasicAttack3,
        Ability,
        Mobility,
        Defensive,
        Utility,
        Ultimate,
    }

    protected PlayerState state = PlayerState.Spawn;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        health = maxHealth;
        moveSpeed = maxMoveSpeed;
    }

    private void Update()
    {
        Debug.Log(state);

        switch (state)
        {
            case PlayerState.Spawn:
                PlayerSpawnState();
                break;
            case PlayerState.Idle:
                PlayerIdleState();
                break;
            case PlayerState.Run:
                PlayerRunState();
                break;
            case PlayerState.Hurt:
                PlayerHurtState(damage);
                break;
            case PlayerState.Death:
                PlayerDeathState();
                break;
            case PlayerState.BasicAttack:
                PlayerBasicAttackState();
                break;
            case PlayerState.BasicAttack2:
                PlayerBasicAttack2State();
                break;
            case PlayerState.BasicAttack3:
                PlayerBasicAttack3State();
                break;
            case PlayerState.Ability:
                PlayerAbilityState();
                break;
            case PlayerState.Mobility:
                PlayerMobilityState();
                break;
            case PlayerState.Defensive:
                PlayerDefensiveState();
                break;
            case PlayerState.Utility:
                PlayerUtilityState();
                break;
            case PlayerState.Ultimate:
                PlayerUltimateState();
                break;
        }

        // Testing
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isPlayerHurt = true;
            state = PlayerState.Hurt;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            isPlayerDead = true;
            state = PlayerState.Death;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            state = PlayerState.Spawn;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            RestoreHealth(1);
        }

        if (animator.GetFloat("Vertical") >= 5)
        {
            GameObject.Find("Sword").GetComponent<SpriteRenderer>().sortingOrder = 1;
            //GameObject.Find("Bow").GetComponent<SpriteRenderer>().sortingOrder = 1;
            //GameObject.Find("Staff").GetComponent<SpriteRenderer>().sortingOrder = 1;
            //GameObject.Find("Dagger").GetComponent<SpriteRenderer>().sortingOrder = 1;
        } else
        {
            GameObject.Find("Sword").GetComponent<SpriteRenderer>().sortingOrder = -1;
            //GameObject.Find("Bow").GetComponent<SpriteRenderer>().sortingOrder = -1;
            //GameObject.Find("Staff").GetComponent<SpriteRenderer>().sortingOrder = -1;
            //GameObject.Find("Dagger").GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (state == PlayerState.Run)
        {
            // Move in direction of movement keys
            rb.MovePosition(rb.position + movement * Time.deltaTime);
        }

        if (canSlide)
        {
            canSlide = false;
            SlideForward();
        }
    }

    public void PlayerSpawnState()
    {
        // Animation
        animator.Play("Spawn");
    }

    public void PlayerIdleState()
    {
        // Animation
        animator.Play("Idle");
        animator.Play("Idle", 1);
        animator.Play("Idle", 2);
        animator.Play("Idle", 3);
        animator.Play("Idle", 4);

        // Tranitions
        MoveKeyPressed();
        BasicAttackKeyPressed();
    }

    public void PlayerRunState()
    {
        // Animation
        animator.Play("Run");
        animator.Play("Run", 1);
        animator.Play("Run", 2);
        animator.Play("Run", 3);
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
        movement = moveInput.normalized * moveSpeed;

        // Transitions
        NoMoveKeyPressed();
        BasicAttackKeyPressed();
    }

    public void PlayerHurtState(float damage)
    {
        if (isPlayerHurt)
        {
            isPlayerHurt = false;
            animator.Play("Hurt", -1, 0f);
            animator.Play("Hurt", 1, 0f);
            animator.Play("Hurt", 2, 0f);
            animator.Play("Hurt", 3, 0f);
            animator.Play("Hurt", 4, 0f);
        }

        if (health <= 0)
        {
            isPlayerDead = true;
            state = PlayerState.Death;
        }
    }

    public void PlayerDeathState()
    {
        if (isPlayerDead)
        {
            isPlayerDead = false;
            animator.Play("Death", -1, 0f);
            animator.Play("Death", 1, 0f);
            animator.Play("Death", 2, 0f);
            animator.Play("Death", 3, 0f);
            animator.Play("Death", 4, 0f);

            Destroy(gameObject, .7f);
        }
    }

    protected virtual void PlayerBasicAttackState()
    {

    }

    public void PlayerBasicAttack2State()
    {

    }

    public void PlayerBasicAttack3State()
    {

    }

    public void PlayerAbilityState()
    {

    }

    public void PlayerMobilityState()
    {

    }

    public void PlayerDefensiveState()
    {

    }

    public void PlayerUtilityState()
    {

    }

    public void PlayerUltimateState()
    {

    }

    // Animation Events

    public void AE_Spawn()
    {
        state = PlayerState.Idle;
    }

    public void AE_Hurt()
    {
        state = PlayerState.Idle;
    }

    // Helper Methods

    protected virtual void SlideForward()
    {
        // Calculates the difference between the mouse position and player position
        angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // If Mouse is outside attack range - Slide
        if (Vector3.Distance(rb.position, cam.ScreenToWorldPoint(Input.mousePosition)) > basicAttackRange)
        {
            // Normalize movement vector and times it by attack move distance
            angleToMouse = angleToMouse.normalized * basicAttackSlideForce;

            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * moveSpeed * Time.deltaTime);
        }

        // If Movement key is held while attacking - Slide
        if (Input.GetKey(upKey) || Input.GetKey(leftKey) || Input.GetKey(downKey) || Input.GetKey(rightKey))
        {
            // Normalize movement vector and times it by attack move distance
            angleToMouse = angleToMouse.normalized * basicAttackSlideForce;

            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        // Player hurt bool - Necessary so hurt state only runs once
        isPlayerHurt = true;

        // Sets player state to hurt state
        state = PlayerState.Hurt;

        // Reduce Health
        health -= damage;

        // Healthbar Lerp
        healthbar.lerpTimer = 0f;
    }

    void RestoreHealth(float healAmount)
    {
        // Restore Health
        health += healAmount;

        // HealthBar Lerp
        healthbar.lerpTimer = 0f;
    }

    // Input

    public void MoveKeyPressed()
    {
        //  Movement Key Pressed
        if (Input.GetKey(upKey) || Input.GetKey(leftKey) || Input.GetKey(downKey) || Input.GetKey(rightKey))
        {
            state = PlayerState.Run;
        }
    }

    public void NoMoveKeyPressed()
    {
        // No Movement Key Pressed
        if (!Input.GetKey(upKey) && !Input.GetKey(leftKey) && !Input.GetKey(downKey) && !Input.GetKey(rightKey))
        {
            state = PlayerState.Idle;
        }
    }

    public void BasicAttackKeyPressed()
    {
        if (Input.GetKey(basicAttackKey) && canBasicAttack)
        {
            state = PlayerState.BasicAttack;
        }
    }
}
