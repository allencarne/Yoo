using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerClassSelection;

public class UpdatePlayerAbilityBar : MonoBehaviour
{
    [SerializeField] GameObject beginnerWithSwordBar;
    [SerializeField] GameObject beginnerWithBowBar;
    [SerializeField] GameObject beginnerWithStaffBar;
    [SerializeField] GameObject beginnerWithDaggerBar;

    [SerializeField] GameObject zephyrAbilityBar;

    private void Update()
    {
        if (PlayerClassSelection.begginerWithSword)
        {
            beginnerWithSwordBar.SetActive(true);
        } else
        {
            beginnerWithSwordBar.SetActive(false);
        }

        if (PlayerClassSelection.begginerWithBow)
        {
            beginnerWithBowBar.SetActive(true);
        }
        else
        {
            beginnerWithBowBar.SetActive(false);
        }

        if (PlayerClassSelection.begginerWithStaff)
        {
            beginnerWithStaffBar.SetActive(true);
        }
        else
        {
            beginnerWithStaffBar.SetActive(false);
        }

        if (PlayerClassSelection.begginerWithDagger)
        {
            beginnerWithDaggerBar.SetActive(true);
        }
        else
        {
            beginnerWithDaggerBar.SetActive(false);
        }

        if (playerClass == PlayerClassSelection.PlayerClass.Zephyr)
        {
            zephyrAbilityBar.SetActive(true);
        } else
        {
            zephyrAbilityBar.SetActive(false);
        }
    }


}
