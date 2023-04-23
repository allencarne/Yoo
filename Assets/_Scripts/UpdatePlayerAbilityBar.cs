using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerClassSelection;

public class UpdatePlayerAbilityBar : MonoBehaviour
{
    [SerializeField] GameObject zephyrAbilityBar;

    private void Update()
    {
        if (playerClass == PlayerClassSelection.PlayerClass.Zephyr)
        {
            zephyrAbilityBar.SetActive(true);
        } else
        {
            zephyrAbilityBar.SetActive(false);
        }
    }
}
