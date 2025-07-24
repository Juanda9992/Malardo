using System;
using TMPro;
using UnityEngine;

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

        requiredScore = Mathf.RoundToInt(blindScoreData.baseScore[anteLevel] * blindScoreData.scoreMultiplier[currentBlindProgress]);
        requiredScoreText.text = requiredScore.ToString();
        blindMoney = blindScoreData.blindMoney[currentBlindProgress];
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
