using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    EquipmentManager equipmentManager;

    public Transform equipmentItemsParent;
    EquipmentInventorySlot[] equipmentSlots;

    private void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChangedCallback += UpdateUI;

        equipmentSlots = equipmentItemsParent.GetComponentsInChildren<EquipmentInventorySlot>();
    }

    void UpdateUI(Equipment newItem, Equipment oldItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        if (newItem != null)
        {
            equipmentSlots[slotIndex].AddItem(newItem);
        } else
        {
            equipmentSlots[slotIndex].ClearSlot();
        }
    }
}
