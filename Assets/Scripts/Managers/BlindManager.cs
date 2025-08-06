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
    [SerializeField] private int currentBlindProgress; //0 to 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI requiredScoreText;
    [SerializeField] private TextMeshProUGUI currentRoundText, currentBetLevelText;
    [SerializeField] private TextMeshProUGUI rewardLabel;
    [SerializeField] private TextMeshProUGUI blindNameText;
    [SerializeField] private Image blindSprite;
    [SerializeField] private Image[] bgImages;
    [SerializeField] private TextMeshProUGUI blindEffectText, blindDescriptionText;

    public event Action OnBlindDefeated;

    private CurrentBlind activeBossBlind = null;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetRequiredScore();
    }

    public void SetRequiredScore()
    {
        blindDescriptionText.text = string.Empty;
        currentRoundText.text = currentRound.ToString();
        currentBetLevelText.text = (anteLevel + 1) + " / 4";

        requiredScore = Mathf.RoundToInt(blindScoreData.baseScore[anteLevel] * blindScoreData.allBlinds[currentBlindProgress].scoreMultiplier);
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
        OnBlindDefeated?.Invoke();
        currentBlindProgress++;
        currentRound++;

        if (currentBlindProgress > 2)
        {
            currentBlindProgress = 0;
            anteLevel++;
        }
        currentRoundText.text = currentRound.ToString();
        SetRequiredScore();

        if (currentBlindProgress == 2)
        {
            SetUpBossBlind();
        }
        else
        {
            ResetBossBlind();
        }
    }

    private void SetUpBossBlind()
    {
        activeBossBlind = blindScoreData.bossBlinds[Random.Range(0, blindScoreData.allBlinds.Length)];
        Debug.Log(activeBossBlind.blindName);

        SetUpBGColor(activeBossBlind.blindColor);

        activeBossBlind.ApplyEffect();
        blindNameText.text = blindScoreData.bossBlinds[currentBlindProgress].blindName;
        blindEffectText.gameObject.SetActive(true);
        blindEffectText.text = activeBossBlind.blindDescription;
        blindDescriptionText.text = activeBossBlind.blindDescription;

        StartCoroutine(nameof(HideBlindText));
    }

    private void ResetBossBlind()
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
