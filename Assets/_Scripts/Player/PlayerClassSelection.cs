using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static PlayerClassSelection;

public class PlayerClassSelection : MonoBehaviour
{
    bool swordEquipped;
    bool bowEquipped;
    bool staffEquipped;
    bool daggerEquipped;

    bool windEquipped;
    bool fireEquipped;
    bool iceEquipped;
    bool electricityEquipped;

    public enum PlayerClass
    {
        Beginner,
        Zephyr,
        WindWalker,
        StormCaster,
        DuskBlade,
        Slayer,
        Incidiary,
        Pyromancer,
        Scorch
    }

    public PlayerClass playerClass = PlayerClass.Beginner;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        switch (playerClass)
        {
            case PlayerClass.Beginner:
                Beginner();
                break;
            case PlayerClass.Zephyr:
                Zephyr(newItem, oldItem);
                break;
        }

        Weapon equippedWeapon = newItem as Weapon;

        if (equippedWeapon != null && equippedWeapon.weaponType == WeaponType.Sword)
        {
            swordEquipped = true;

            bowEquipped = false;
            staffEquipped = false;
            daggerEquipped = false;
        }

        if (equippedWeapon != null && equippedWeapon.weaponType == WeaponType.Bow)
        {
            bowEquipped = true;

            swordEquipped = false;
            staffEquipped = false;
            daggerEquipped = false;
        }

        if (equippedWeapon != null && equippedWeapon.weaponType == WeaponType.Staff)
        {
            staffEquipped = true;

            swordEquipped = false;
            bowEquipped = false;
            daggerEquipped = false;
        }

        if (equippedWeapon != null && equippedWeapon.weaponType == WeaponType.Dagger)
        {
            daggerEquipped = true;

            swordEquipped = false;
            bowEquipped = false;
            staffEquipped = false;
        }

        Element equippedElement = newItem as Element;

        if (equippedElement != null && equippedElement.elementType == ElementType.Wind)
        {
            windEquipped = true;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Fire)
        {
            fireEquipped = true;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Ice)
        {
            iceEquipped = true;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Electricity)
        {
            electricityEquipped = true;
        }
    }

    void Beginner()
    {
        
    }

    void Zephyr(Equipment newItem, Equipment oldItem)
    {
        Weapon equippedWeapon = newItem as Weapon;

        if (equippedWeapon != null && equippedWeapon.weaponType == WeaponType.Sword)
        {
            swordEquipped = true;
        }
        else
        {
            swordEquipped = false;
        }

        Element equippedElement = newItem as Element;

        if (equippedElement != null && equippedElement.elementType == ElementType.Wind)
        {
            windEquipped = true;
        }
        else
        {
            windEquipped = false;
        }
    }

    public void HUH(Equipment newItem, Equipment oldItem)
    {

    }
}
