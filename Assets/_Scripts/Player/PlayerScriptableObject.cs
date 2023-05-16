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
    public float windSlashCastTime;
    public float windSlashDuration;
    public float windSlashCoolDown;
    public float windSlashDamage;
    public float windSlashSlideForce;
    public float windSlashKnockBackForce;

    [Header("Zephyr Slicing Winds")]
    public float slicingWindsCastTime;
    public float slicingWindsDuration;
    public float slicingWindsCoolDown;
    public float slicingWindsDamage;
    public float slicingWindsSlideForce;
    public float slicingWindsKnockBackForce;
    public float slicingWindsSlideDuration;

    [Header("Zephyr Tempest Charge")]
    public float tempestChargeCastTime;
    public float tempestChargeDuration;
    public float tempestChargeCoolDown;
    public float tempestChargeDamage;
    public float tempestChargeVelocity;

    [Header("Zephyr Parry Strike")]
    public float parryStrikeCastTime;
    public float parryStrikeDuration;
    public float parryStrikeCoolDown;
    public float parryStrikeDamage;

    [Header("Zephyr Heavy Blow")]
    public float heavyBlowCastTime;
    public float heavyBlowDuration;
    public float heavyBlowCoolDown;
    public float heavyBlowDamage;

    [Header("Zephyr Engulf")]
    public float engulfCastTime;
    public float engulfDuration;
    public float engulfCoolDown;
    public float engulfDamage;
}
