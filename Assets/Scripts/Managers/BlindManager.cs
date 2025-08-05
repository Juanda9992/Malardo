using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public event Action OnBlindDefeated;
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
        currentRoundText.text = currentRound.ToString();
        currentBetLevelText.text = (anteLevel + 1) + " / 4";

        requiredScore = Mathf.RoundToInt(blindScoreData.baseScore[anteLevel] * blindScoreData.allBlinds[currentBlindProgress].scoreMultiplier);
        blindMoney = blindScoreData.allBlinds[currentBlindProgress].blindMoney;

        SetUpUI();
        SetUpBGColor(blindScoreData.allBlinds[currentBlindProgress].blindColor);

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
    }

    private void SetUpBossBlind()
    {
        CurrentBlind currentBlind = blindScoreData.bossBlinds[Random.Range(0, blindScoreData.allBlinds.Length)];
        Debug.Log(currentBlind.blindName);

        SetUpBGColor(currentBlind.blindColor);

        currentBlind.ApplyEffect();
        blindNameText.text = blindScoreData.bossBlinds[currentBlindProgress].blindName;
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }

}
