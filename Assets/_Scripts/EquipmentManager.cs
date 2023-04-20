using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallback;

    Inventory inventory;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    private void Start()
    {
        inventory = Inventory.instance;
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

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
