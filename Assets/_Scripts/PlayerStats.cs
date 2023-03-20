using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public string PlayerName;
    public int playerLevel;
    public int playerExperience;
    public PlayerClass playerClass;
    public float playerMoveSpeed;

}

public enum PlayerClass
{
    Warrior,
    Mage,
    Archer
}
