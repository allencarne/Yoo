using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
}
