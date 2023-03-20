using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState();
    public PlayerBasicAttackState basicAttackState = new PlayerBasicAttackState();
    public PlayerHurtState hurtState = new PlayerHurtState();
    [SerializeField] Player player;
    public Player Player => player;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;

    [Header("Keys")]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode basicAttackKey;

    public bool AnyMoveKeyPressed => Input.GetKey(upKey) || Input.GetKey(leftKey) || Input.GetKey(downKey) || Input.GetKey(rightKey);

    public bool BasicAttackKeyPressed => Input.GetKey(basicAttackKey);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Starting State for the State Machine
        currentState = idleState;

        // "this" is a reference to the context (this Exact Monobehaviour script)
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Will call any logic in Update State from the current state every frame
        currentState.UpdateState(this);
    }

    public void ChangeState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
