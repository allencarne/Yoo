using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
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
}
