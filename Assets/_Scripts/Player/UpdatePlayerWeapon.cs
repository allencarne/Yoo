using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject staff;
    [SerializeField] GameObject dagger;

    private void Update()
    {
        if (PlayerClassSelection.swordEquipped)
        {
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
        }

        if (PlayerClassSelection.bowEquipped)
        {
            bow.SetActive(true);
        }
        else
        {
            bow.SetActive(false);
        }

        if (PlayerClassSelection.staffEquipped)
        {
            staff.SetActive(true);
        }
        else
        {
            staff.SetActive(false);
        }

        if (PlayerClassSelection.daggerEquipped)
        {
            dagger.SetActive(true);
        }
        else
        {
            dagger.SetActive(false);
        }
    }
}
