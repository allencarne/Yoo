using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int enemyLevel;
    public int enemyHealth;
    public int enemyMaxHealth;
    public float enemyMoveSpeed;
    public int enemyExperience;
    public EnemyType enemyType;
}

public enum EnemyType
{
    Snail,
    Mushroom,
    Slime
}
