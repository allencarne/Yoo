using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    EquipmentInventorySlot inventorySlot;

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallback += UpdateUI;
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            //inventorySlot.AddItem();
        }
    }
}
