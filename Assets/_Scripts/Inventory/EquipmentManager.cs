using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallback;

    Inventory inventory;

    // Array of all the items we currently have equipt
    public Equipment[] currentEquipment;

    private void Start()
    {
        inventory = Inventory.instance;

        // Get the number of equipment slots
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        // Define Array Length
        currentEquipment = new Equipment[numberOfSlots];
    }

    public void Equip(Equipment newItem)
    {
        // Gets the index that the new item is supposed to be slotted into
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        // If there is already a piece of equipment in the slot
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        // Set new item
        currentEquipment[slotIndex] = newItem;
    }

    public void UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChangedCallback != null)
            {
                onEquipmentChangedCallback.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            UnequipAll();
        }
    }
}
