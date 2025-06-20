using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private float chips = 0;
    [SerializeField] private float mult = 1;
    [SerializeField] private float roundScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        instance = this;
    }

    public void AddChips(int value)
    {
        chips += value;
    }

    public void AddMult(int value)
    {
        mult += value;
    }

    public void CalculateScore()
    {
        float currentScore = chips * mult;

        roundScore += currentScore;

        scoreText.text = roundScore.ToString();
    }

    public void ResetScore()
    {
        chips = 0;
        mult = 0;
    }
}
