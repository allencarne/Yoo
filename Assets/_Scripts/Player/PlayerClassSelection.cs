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

    [SerializeField] Beginner beginner;
    [SerializeField] Zephyr zephyr;

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
        Scorch,
        IceSword,
        IceBow,
        IceStaff,
        IceDagger,
        ElectricitySword,
        ElectricityBow,
        ElectricityStaff,
        ElectricityDagger
    }

    public PlayerClass playerClass = PlayerClass.Beginner;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    private void Update()
    {
        Debug.Log(playerClass);

        #region Wind

        // Wind Sword
        if (swordEquipped && windEquipped)
        {
            playerClass = PlayerClass.Zephyr;
        }

        // Wind Bow
        if (bowEquipped && windEquipped)
        {
            playerClass = PlayerClass.WindWalker;
        }

        // Wind Staff
        if (staffEquipped && windEquipped)
        {
            playerClass = PlayerClass.StormCaster;
        }

        // Wind Dagger
        if (daggerEquipped && windEquipped)
        {
            playerClass = PlayerClass.DuskBlade;
        }

        #endregion

        #region Fire
        // Fire Sword
        if (swordEquipped && fireEquipped)
        {
            playerClass = PlayerClass.Slayer;
        }

        // Fire Bow
        if (bowEquipped && fireEquipped)
        {
            playerClass = PlayerClass.Incidiary;
        }

        // Fire Staff
        if (staffEquipped && fireEquipped)
        {
            playerClass = PlayerClass.Pyromancer;
        }

        // Fire Dagger
        if (daggerEquipped && fireEquipped)
        {
            playerClass = PlayerClass.Scorch;
        }

        #endregion

        #region Ice

        // Ice Sword
        if (swordEquipped && iceEquipped)
        {
            playerClass = PlayerClass.IceSword;
        }

        // Ice Bow
        if (bowEquipped && iceEquipped)
        {
            playerClass = PlayerClass.IceBow;
        }

        // Ice Staff
        if (staffEquipped && iceEquipped)
        {
            playerClass = PlayerClass.IceStaff;
        }

        // Ice Dagger
        if (daggerEquipped && iceEquipped)
        {
            playerClass = PlayerClass.IceDagger;
        }

        #endregion

        #region Electricity
        // Electricity Sword
        if (swordEquipped && electricityEquipped)
        {
            playerClass = PlayerClass.ElectricitySword;
        }

        // Electricity Bow
        if (bowEquipped && electricityEquipped)
        {
            playerClass = PlayerClass.ElectricityBow;
        }

        // Electricity Staff
        if (staffEquipped && electricityEquipped)
        {
            playerClass = PlayerClass.ElectricityStaff;
        }

        // Electricity Dagger
        if (daggerEquipped && electricityEquipped)
        {
            playerClass = PlayerClass.ElectricityDagger;
        }

        #endregion
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        #region Weapons

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

        #endregion

        #region Elements

        Element equippedElement = newItem as Element;

        if (equippedElement != null && equippedElement.elementType == ElementType.Wind)
        {
            windEquipped = true;

            fireEquipped = false;
            iceEquipped = false;
            electricityEquipped = false;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Fire)
        {
            fireEquipped = true;

            windEquipped = false;
            iceEquipped = false;
            electricityEquipped = false;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Ice)
        {
            iceEquipped = true;

            windEquipped = false;
            fireEquipped = false;
            electricityEquipped = false;
        }

        if (equippedElement != null && equippedElement.elementType == ElementType.Electricity)
        {
            electricityEquipped = true;

            windEquipped = false;
            fireEquipped = false;
            iceEquipped = false;
        }

        #endregion
    }
}
