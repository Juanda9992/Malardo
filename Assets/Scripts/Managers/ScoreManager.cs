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

        ResetChipsAndMult();
    }

    void Start()
    {
        BlindManager.instance.OnBlindDefeated += ResetRoundScore;
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
    public void SetChips(int value)
    {
        chips = value;
        chipsText.text = chips.ToString();
    }

    public void SetMult(int value)
    {
        mult = value;
        multText.text = mult.ToString();
    }

    public void MultiplyMulti(int multiplier)
    {
        mult *= multiplier;
        multText.text = mult.ToString();
    }

    public void CalculateScore()
    {
        float currentScore = chips * mult;

        roundScore += currentScore;

        scoreText.text = roundScore.ToString();

        CheckBlindWin();
    }

    private void CheckBlindWin()
    {
        if (BlindManager.instance.BlindDefeated((int)roundScore))
        {
            BlindManager.instance.IncreaseBetlevel();
        }
        else
        {
            if (HandManager.instance.hands == 0)
            {
                Debug.Log("Game lost");
            }
        }
    }

    public void ResetChipsAndMult()
    {
        chips = 0;
        mult = 1;


        chipsText.text = chips.ToString();
        multText.text = mult.ToString();
    }

    private void ResetRoundScore()
    {
        ResetChipsAndMult();
        roundScore = 0;
        scoreText.text = roundScore.ToString();
    }
}
