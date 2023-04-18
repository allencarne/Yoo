using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryBar : MonoBehaviour
{
    Player player;

    [SerializeField] Image furyBarFront;
    [SerializeField] Image furyBarBack;
    [SerializeField] Canvas playerUI;
    [HideInInspector] public float chipSpeed = 2f;
    [HideInInspector] public float lerpTimer;

    public void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Start()
    {
        //player.fury = player.maxFury;
    }

    public void Update()
    {
        player.fury = Mathf.Clamp(player.fury, 0, player.maxFury);

        UpdateFuryUI();
    }

    void UpdateFuryUI()
    {
        float fillFront = furyBarFront.fillAmount;
        float fillBack = furyBarBack.fillAmount;
        float furyFraction = player.fury / player.maxFury;

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
