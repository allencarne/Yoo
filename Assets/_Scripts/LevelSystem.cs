using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    PlayerManager playerManager;

    //public int level;
    //public float currentXp;
    //public float requiredXp;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    Image frontXpBar;
    Image backXpBar;
    public TextMeshProUGUI levelText;
    TextMeshProUGUI experienceText;

    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    private void Awake()
    {
        playerManager = PlayerManager.instance;

        frontXpBar = GameObject.Find("Exp Bar Fill Front").GetComponent<Image>();
        backXpBar = GameObject.Find("Exp Bar Fill Back").GetComponent<Image>();
        experienceText = GameObject.Find("Exp Text").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        frontXpBar.fillAmount = playerManager.player_SO.currentExperience / playerManager.player_SO.requiredExperience;
        backXpBar.fillAmount = playerManager.player_SO.currentExperience / playerManager.player_SO.requiredExperience;

        playerManager.player_SO.requiredExperience = CalculateRequiredXp();

        levelText.text = playerManager.player_SO.playerLevel.ToString();
    }

    void Update()
    {
        UpdateXpUI();

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperienceFlatRate(20);
        }

        if (playerManager.player_SO.currentExperience > playerManager.player_SO.requiredExperience)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = playerManager.player_SO.currentExperience / playerManager.player_SO.requiredExperience;
        float FXP = frontXpBar.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 0.5)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }

        experienceText.text = playerManager.player_SO.currentExperience + "/" + playerManager.player_SO.requiredExperience;
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        playerManager.player_SO.currentExperience += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        playerManager.player_SO.playerLevel++;

        // Inrease Player Stats

        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;

        playerManager.player_SO.currentExperience = Mathf.RoundToInt(playerManager.player_SO.currentExperience - playerManager.player_SO.requiredExperience);

        playerManager.player_SO.requiredExperience = CalculateRequiredXp();

        levelText.text = playerManager.player_SO.playerLevel.ToString();
    }

    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= playerManager.player_SO.playerLevel; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }
}
