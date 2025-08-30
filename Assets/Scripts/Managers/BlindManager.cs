using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlindManager : MonoBehaviour
{
    public static BlindManager instance;
    public int anteLevel;
    public int blindMoney;
    public int requiredScore;
    public int currentRound = 1;
    [SerializeField] private BlindScoreData blindScoreData;
    public int currentBlindProgress; //0 to 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI requiredScoreText;
    [SerializeField] private TextMeshProUGUI currentRoundText, currentBetLevelText;
    [SerializeField] private TextMeshProUGUI rewardLabel;
    [SerializeField] private TextMeshProUGUI blindNameText;
    [SerializeField] private Image blindSprite;
    [SerializeField] private Image[] bgImages;
    [SerializeField] private TextMeshProUGUI blindEffectText, blindDescriptionText;

    public event Action OnBlindDefeated, OnBlindSelected;

    public CurrentBlind activeBossBlind = null;
    void Awake()
    {
        instance = this;
    }

    public void SetRequiredScore()
    {
        blindDescriptionText.text = string.Empty;
        currentRoundText.text = currentRound.ToString();
        currentBetLevelText.text = (anteLevel + 1) + " / 4";

        int newScore = Mathf.RoundToInt(blindScoreData.baseScore[anteLevel] * blindScoreData.allBlinds[currentBlindProgress].scoreMultiplier);
        requiredScore = newScore;
        blindMoney = blindScoreData.allBlinds[currentBlindProgress].blindMoney;

        SetUpUI();
        SetUpBGColor(blindScoreData.allBlinds[currentBlindProgress].blindColor);

    }

    public void SetCustomRequiredScore(int newScore)
    {
        requiredScore = newScore;
        requiredScoreText.text = requiredScore.ToString();
    }

    private void SetUpBGColor(Color currentBlindColor)
    {
        Color blindColor = currentBlindColor;
        BackgroundManager.instance.SetBgColor(blindColor * 0.6f);

        foreach (var bg in bgImages)
        {
            float previousAlpha = bg.color.a;
            blindColor.a = previousAlpha;

            bg.color = blindColor;
        }
    }
    private void SetUpUI()
    {
        requiredScoreText.text = requiredScore.ToString();
        rewardLabel.text = "Reward <color=yellow><b>";
        for (int i = 0; i < blindMoney; i++)
        {
            rewardLabel.text += "$";
        }

        rewardLabel.text += "</b></color>";

        blindSprite.sprite = blindScoreData.allBlinds[currentBlindProgress].blindSprite;

        blindNameText.text = blindScoreData.allBlinds[currentBlindProgress].blindName;
    }

    public bool BlindDefeated(int scoreToCompare)
    {
        return scoreToCompare >= requiredScore;
    }

    [ContextMenu("After Shop")]
    public void IncreaseBetlevel()
    {

        currentRoundText.text = currentRound.ToString();

        if (currentBlindProgress == 2)
        {
            SetUpBossBlind();
        }
        else
        {
            ResetBossBlind();
            SetRequiredScore();
        }

        OnBlindSelected?.Invoke();
    }

    public void SetBlindDefeated()
    {
        PokerHandLevelStorage.instance.ResetHandsPlayedInRound();
        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnBlindDefeated));
        OnBlindDefeated?.Invoke();
        currentBlindProgress++;
        currentRound++;

        if (currentBlindProgress > 2)
        {
            currentBlindProgress = 0;
            anteLevel++;
            GameObject.FindObjectOfType<BlindSelector>().GenerateRoundBlinds();
        }
    }

    public void SetUpBossBlind(CurrentBlind currentBlind = null)
    {
        if (currentBlind != null)
        {
            activeBossBlind = currentBlind;
        }
        else
        {
            activeBossBlind = GameObject.FindObjectOfType<BlindSelector>().GetCurrentBossBlind();
        }
        Debug.Log(activeBossBlind.blindName);

        SetUpBGColor(activeBossBlind.blindColor);

        activeBossBlind.ApplyEffect();
        blindNameText.text = blindScoreData.bossBlinds[currentBlindProgress].blindName;
        blindEffectText.gameObject.SetActive(true);
        blindEffectText.text = activeBossBlind.blindDescription;
        blindDescriptionText.text = activeBossBlind.blindDescription;

        StartCoroutine(nameof(HideBlindText));
    }
    public void ShowInvalidateMessage()
    {
        blindEffectText.gameObject.SetActive(true);
        blindEffectText.text = "Won´t Play";
        StartCoroutine(nameof(HideBlindText));
    }

    public void ResetBossBlind()
    {
        if (activeBossBlind == null)
        {
            return;
        }

        activeBossBlind.RevertEffect();
    }

    private IEnumerator HideBlindText()
    {
        yield return new WaitForSeconds(5);
        blindEffectText.gameObject.SetActive(false);
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }

    public int GetRoundBaseScore()
    {
        return blindScoreData.baseScore[anteLevel];
    }

}
