using System;
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

        Color blindColor = blindScoreData.allBlinds[currentBlindProgress].blindColor;

        blindColor *= 0.6f;
        BackgroundManager.instance.SetBgColor(blindColor);

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
        foreach (var bg in bgImages)
        {
            float previousAlpha = bg.color.a;
            Color blindColor = blindScoreData.allBlinds[currentBlindProgress].blindColor;
            blindColor.a = previousAlpha;

            bg.color = blindColor;

        }

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
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }

}
