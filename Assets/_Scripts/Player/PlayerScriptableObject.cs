using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("Player")]
    [SerializeField] string playerName;
    [SerializeField] int playerLevel;
    [SerializeField] float currentExperience;
    [SerializeField] float requiredExperience;

    public float health;
    public float maxHealth;
    public float movementSpeed;
    public float maxMovementSpeed;
    public float attackDamage;
    public float attackSpeed;

    // Rescources
    public float fury;
    public float maxFury;

    [Header("Zephyr Wind Slash")]
    public GameObject windSlashPrefab;
    public GameObject windSlash2Prefab;
    public GameObject windSlash3Prefab;
    public float windSlashCastTime;
    public float windSlashDuration;
    public float windSlashCoolDown;
    public float windSlashDamage;
    public float windSlashSlideForce;
    public float windSlashKnockBackForce;
    public float windSlash3Damage;
    public float windSlash3KnockBackForce;

    [Header("Zephyr Slicing Winds")]
    public GameObject slicingWindsPrefab;
    public float slicingWindsCastTime;
    public float slicingWindsDuration;
    public float slicingWindsCoolDown;
    public float slicingWindsDamage;
    public float slicingWindsSlideForce;
    public float slicingWindsKnockBackForce;
    public float slicingWindsSlideDuration;

    [Header("Zephyr Gust Charge")]
    public GameObject gustChargePrefab;
    public float gustChargeCastTime;
    public float gustChargeDuration;
    public float gustChargeCoolDown;
    public float gustChargeDamage;
    public float gustChargeVelocity;

    [Header("Zephyr Parry Strike")]
    public GameObject parryStrikeShieldPrefab;
    public GameObject parryStrikePrefab;
    public float parryStrikeCastTime;
    public float parryStrikeDuration;
    public float parryStrikeCoolDown;
    public float parryStrikeDamage;
    public float parryStrikeKnockBackForce;

    [Header("Zephyr Whirling Slash")]
    public GameObject whirlingSlashPrefab;
    public float whirlingSlashCastTime;
    public float whirlingSlashDuration;
    public float whirlingSlashCoolDown;
    public float whirlingSlashDamage;

    [Header("Zephyr Zephyr's Fury")]
    public GameObject zephyrsFuryPrefab;
    public float zephyrsFuryCastTime;
    public float zephyrsFuryDuration;
    public float zephyrsFuryCoolDown;
    public float zephyrsFuryDamage;
    public float zephyrsFuryPullForce;
}
