using System;
using TMPro;
using UnityEngine;

public class BlindManager : MonoBehaviour
{
    public static BlindManager instance;
    public int anteLevel;
    public int blindMoney;
    public int requiredScore;
    public int curerntRound;
    [SerializeField] private BlindScoreData blindScoreData;
    [SerializeField] private int currentBlindProgress; //0 to 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI requiredScoreText;

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
        curerntRound++;

        if (currentBlindProgress > 2)
        {
            currentBlindProgress = 0;
            anteLevel++;
        }

        SetRequiredScore();
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }

}
