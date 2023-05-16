using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [HideInInspector] protected PlayerManager playerManager;

    [Header("Components")]
    [HideInInspector] protected Rigidbody2D rb;
    [HideInInspector] protected Animator animator;
    [HideInInspector] protected Camera cam;
    [HideInInspector] PlayerKeys keys;
    [HideInInspector] HealthBar healthbar;
    [HideInInspector] FuryBar furybar;
    [SerializeField] protected Transform aimer;
    [SerializeField] GameObject floatingTextDamage;
    [SerializeField] GameObject floatingTextHeal;

    [Header("Variables")]
    //[SerializeField] protected float basicAttackSlideForce;
    [HideInInspector] protected float basicAttackRange = 10.2f;
    [HideInInspector] protected Vector2 angleToMouse;
    [HideInInspector] protected bool canSlide = false;
    [HideInInspector] protected Vector2 movement;
    [HideInInspector] bool isPlayerHurt = false;
    [HideInInspector] bool isPlayerDead = false;
    [HideInInspector] float damage;
    [HideInInspector] bool doneSpawning;

    protected bool canBasicAttack = true;
    protected bool canBasicAttack2 = false;
    protected bool canBasicAttack3 = false;
    protected bool canAbility = true;
    protected bool canMobility = true;
    protected bool canDefensive = true;
    protected bool canUtility = true;
    protected bool canUltimate = true;

    public static event System.Action OnPlayerDeath;

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
        playerManager = PlayerManager.instance;

        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        keys = GetComponentInParent<PlayerKeys>();
        cam = Camera.main;
        healthbar = GetComponentInParent<HealthBar>();
        furybar = GetComponentInParent<FuryBar>();
    }

    protected virtual void Start()
    {
        playerManager.player_SO.health = playerManager.player_SO.maxHealth;
        playerManager.player_SO.movementSpeed = playerManager.player_SO.maxMovementSpeed;
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
        /*
       // Sorting Order
       if (animator.GetFloat("Vertical") >= 5)
       {
           sword.GetComponent<SpriteRenderer>().sortingOrder = 1;
       }
       else
       {
           sword.GetComponent<SpriteRenderer>().sortingOrder = -1;
       }
        */

        Testing();
    }

    protected virtual void FixedUpdate()
    {
        if (state == PlayerState.Run)
        {
            // Move in direction of movement keys
            rb.MovePosition(rb.position + movement * Time.deltaTime);
        }

        /*
        if (canSlide)
        {
            canSlide = false;
            SlideForward();
        }
        */
    }

    public void PlayerSpawnState()
    {
        // Animation
        animator.Play("Spawn");

        if (doneSpawning)
        {
            doneSpawning = false;
            state = PlayerState.Idle;
        }

        StartCoroutine(SpawnAnimationDruation());
    }

    IEnumerator SpawnAnimationDruation()
    {
        yield return new WaitForSeconds(.5f);

        doneSpawning = true;
        //Debug.Log("test");
        //state = PlayerState.Idle;
    }

    protected virtual void PlayerIdleState()
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
        AbilityKeyPressed();
        MobilityKeyPressed();
        DefensiveKeyPressed();
        UtilityKeyPressed();
        UltimateKeyPressed();
    }

    protected virtual void PlayerRunState()
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
        movement = moveInput.normalized * playerManager.player_SO.movementSpeed;

        // Transitions
        NoMoveKeyPressed();
        BasicAttackKeyPressed();
        AbilityKeyPressed();
        MobilityKeyPressed();
        DefensiveKeyPressed();
        UtilityKeyPressed();
        UltimateKeyPressed();
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

            StartCoroutine(HurtAnimationDuration());
        }

        if (playerManager.player_SO.health <= 0)
        {
            isPlayerDead = true;
            state = PlayerState.Death;
        }
    }

    IEnumerator HurtAnimationDuration()
    {
        yield return new WaitForSeconds(.6f);

        state = PlayerState.Idle;
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

            OnPlayerDeath?.Invoke();

            //Destroy(gameObject, .5f);
        }
    }

    protected virtual void PlayerBasicAttackState()
    {

    }

    protected virtual void PlayerBasicAttack2State()
    {

    }

    protected virtual void PlayerBasicAttack3State()
    {

    }

    protected virtual void PlayerAbilityState()
    {

    }

    protected virtual void PlayerMobilityState()
    {

    }

    protected virtual void PlayerDefensiveState()
    {

    }

    protected virtual void PlayerUtilityState()
    {

    }

    protected virtual void PlayerUltimateState()
    {

    }

    #region Helper Methods

    bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    protected virtual void AngleToMouse()
    {
        // Calculates angle from mouse position and player position
        angleToMouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    protected virtual void SetAnimationDirection()
    {
        // Set Attack Animation Depending on Mouse Position
        animator.SetFloat("Aim Horizontal", angleToMouse.x);
        animator.SetFloat("Aim Vertical", angleToMouse.y);
        // Set Idle to last attack position
        animator.SetFloat("Horizontal", angleToMouse.x);
        animator.SetFloat("Vertical", angleToMouse.y);
    }

    protected virtual void SlideForward(float slideAmount)
    {
        // If Mouse is outside attack range - Slide
        if (Vector3.Distance(rb.position, cam.ScreenToWorldPoint(Input.mousePosition)) > basicAttackRange)
        {
            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * slideAmount * Time.deltaTime);
        }

        // If Movement key is held while attacking - Slide
        if (Input.GetKey(keys.upKey) || Input.GetKey(keys.leftKey) || Input.GetKey(keys.downKey) || Input.GetKey(keys.rightKey))
        {
            // Slide in Attack Direction
            rb.MovePosition(rb.position + angleToMouse * slideAmount * Time.deltaTime);
        }
    }

    protected virtual void PauseAimer()
    {
        AimIndicator.pauseDirection = true;
    }

    public void TakeDamage(float damage)
    {
        // Player hurt bool - Necessary so hurt state only runs once
        isPlayerHurt = true;

        // Sets player state to hurt state
        state = PlayerState.Hurt;

        // Damage Text
        ShowDamage(damage.ToString());

        // Reduce Health
        playerManager.player_SO.health -= damage;

        // Healthbar Lerp
        healthbar.lerpTimer = 0f;
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

        // Restore Health
        playerManager.player_SO.health += healAmount;

        // HealthBar Lerp
        healthbar.lerpTimer = 0f;
    }

    void ShowHeal(string text)
    {
        if (floatingTextHeal)
        {
            GameObject prefab = Instantiate(floatingTextHeal, transform.position, Quaternion.identity, transform);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public void GainFury(float furyGainAmount)
    {
        playerManager.player_SO.fury += furyGainAmount;

        furybar.lerpTimer = 0f;
    }

    public void LoseFury(float furyLoseAmount)
    {
        playerManager.player_SO.fury -= furyLoseAmount;

        furybar.lerpTimer = 0f;
    }

    #endregion

    #region Input

    public void MoveKeyPressed()
    {
        //  Movement Key Pressed
        if (Input.GetKey(keys.upKey) || Input.GetKey(keys.leftKey) || Input.GetKey(keys.downKey) || Input.GetKey(keys.rightKey))
        {
            state = PlayerState.Run;
        }
    }

    public void NoMoveKeyPressed()
    {
        // No Movement Key Pressed
        if (!Input.GetKey(keys.upKey) && !Input.GetKey(keys.leftKey) && !Input.GetKey(keys.downKey) && !Input.GetKey(keys.rightKey))
        {
            state = PlayerState.Idle;
        }
    }

    public void BasicAttackKeyPressed()
    {
        if (!isMouseOverUI())
        {
            if (Input.GetKey(keys.basicAttackKey) && canBasicAttack)
            {
                state = PlayerState.BasicAttack;
            }
        }
    }

    protected virtual void BasicAttack2KeyPressed()
    {
        if (!isMouseOverUI())
        {
            if (Input.GetKey(keys.basicAttackKey) && canBasicAttack2)
            {
                state = PlayerState.BasicAttack2;
            }
        }
    }

    protected virtual void BasicAttack3KeyPressed()
    {
        if (!isMouseOverUI())
        {
            if (Input.GetKey(keys.basicAttackKey) && canBasicAttack3)
            {
                state = PlayerState.BasicAttack3;
            }
        }
    }

    protected virtual void AbilityKeyPressed()
    {
        if (!isMouseOverUI())
        {
            if (Input.GetKey(keys.abilityKey) && canAbility)
            {
                state = PlayerState.Ability;
            }
        }
    }

    protected virtual void MobilityKeyPressed()
    {
        if (Input.GetKey(keys.mobilityKey) && canMobility)
        {
            state = PlayerState.Mobility;
        }
    }

    protected virtual void DefensiveKeyPressed()
    {
        if (Input.GetKey(keys.defensiveKey) && canDefensive)
        {
            state = PlayerState.Defensive;
        }
    }

    protected virtual void UtilityKeyPressed()
    {
        if (Input.GetKey(keys.utilityKey) && canUtility)
        {
            state = PlayerState.Utility;
        }
    }

    protected virtual void UltimateKeyPressed()
    {
        if (Input.GetKey(keys.ultimateKey) && canUltimate)
        {
            state = PlayerState.Ultimate;
        }
    }

    #endregion

    // Testing

    void Testing()
    {
        // Testing - Damage
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1);
        }

        // Testing - Heal
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestoreHealth(1);
        }

        // Testing - Gain Fury
        if (Input.GetKeyDown(KeyCode.U))
        {
            GainFury(1);
        }

        // Testing - Lose Fury
        if (Input.GetKeyDown(KeyCode.I))
        {
            LoseFury(1);
        }
    }
}
