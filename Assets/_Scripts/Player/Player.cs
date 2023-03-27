using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    enum PlayerState
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

    PlayerState state = PlayerState.Spawn;

    private void Update()
    {
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
                PlayerHurtState();
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
    }

    public void PlayerSpawnState()
    {

    }

    public void PlayerIdleState()
    {

    }

    public void PlayerRunState()
    {

    }

    public void PlayerHurtState()
    {

    }

    public void PlayerDeathState()
    {

    }

    public void PlayerBasicAttackState()
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
}
