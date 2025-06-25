using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlindManager : MonoBehaviour
{
    public static BlindManager instance;
    public int anteLevel;
    [SerializeField] private int requiredScore;
    [SerializeField] private BlindScoreData blindScoreData;
    [SerializeField] private int currentBlindProgress; //0 to 2;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI requiredScoreText;
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
    }

    public bool BlindDefeated(int scoreToCompare)
    {
        return scoreToCompare > requiredScore;
    }

    public void IncreaseBetlevel()
    {
        currentBlindProgress++;

        if (currentBlindProgress > 2)
        {
            currentBlindProgress = 0;
            anteLevel++;
        }

        SetRequiredScore();
    }

}
