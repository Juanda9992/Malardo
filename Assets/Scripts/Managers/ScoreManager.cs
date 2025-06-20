using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private float chips = 0;
    [SerializeField] private float mult = 1;
    [SerializeField] private float roundScore;

    [SerializeField] private TextMeshProUGUI scoreText, chipsText, multText;
    void Awake()
    {
        instance = this;

        ResetScore();
    }

    public void AddChips(int value)
    {
        chips += value;
        chipsText.text = chips.ToString();
    }

    public void AddMult(int value)
    {
        mult += value;
        multText.text = mult.ToString();
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
        mult = 1;


        chipsText.text = chips.ToString();
        multText.text = mult.ToString();
    }
}
