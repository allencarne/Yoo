using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "ScriptableObjects/Inventory/Equipment/Element")]
public class Element : Equipment
{
    public ElementType elementType;
}

public enum ElementType
{
    Wind,
    Fire,
    Ice,
    Electricity
}
