using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryBar : MonoBehaviour
{
    [SerializeField] PlayerScriptableObject SO_Player;

    [SerializeField] Image furyBarFront;
    [SerializeField] Image furyBarBack;
    [SerializeField] Canvas playerUI;
    [HideInInspector] public float chipSpeed = 2f;
    [HideInInspector] public float lerpTimer;

    public void Update()
    {
        SO_Player.fury = Mathf.Clamp(SO_Player.fury, 0, SO_Player.maxFury);

        UpdateFuryUI();
    }

    void UpdateFuryUI()
    {
        float fillFront = furyBarFront.fillAmount;
        float fillBack = furyBarBack.fillAmount;
        float furyFraction = SO_Player.fury / SO_Player.maxFury;

        if (fillBack > furyFraction)
        {
            furyBarFront.fillAmount = furyFraction;
            furyBarBack.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            furyBarBack.fillAmount = Mathf.Lerp(fillBack, furyFraction, percentComplete);
        }

        if (fillFront < furyFraction)
        {
            furyBarBack.color = Color.white;
            furyBarBack.fillAmount = furyFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            furyBarFront.fillAmount = Mathf.Lerp(fillFront, furyBarBack.fillAmount, percentComplete);
        }
    }
}
