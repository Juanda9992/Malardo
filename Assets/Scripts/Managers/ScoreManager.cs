using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private float chips = 0;
    [SerializeField] private float mult = 1;
    [SerializeField] private float roundScore;

    [SerializeField] private TextMeshProUGUI scoreText, chipsText, multText,totalScoreText;
    void Awake()
    {
        instance = this;

        ResetChipsAndMult();
    }

    void Start()
    {
        BlindManager.instance.OnBlindDefeated += ResetRoundScore;
        scoreText.text = "0";
    }

    public void AddChips(int value)
    {
        chips += value;
        chipsText.text = chips.ToString();
    }

    public void AddMult(float value)
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

    public void MultiplyMulti(float multiplier)
    {
        mult *= multiplier;
        multText.text = mult.ToString("0.##");
    }

    public void CalculateScore()
    {
        float currentScore = chips * mult;

        roundScore += currentScore;

        totalScoreText.text = roundScore.ToString();
        scoreText.text = currentScore.ToString();

        CheckBlindWin();
    }

    private void CheckBlindWin()
    {
        if (BlindManager.instance.BlindDefeated((int)roundScore))
        {
            if (EndGameManager.instance.ReachedEndGame())
            {
                EndGameManager.instance.SetEndGame();
                return;
            }
            StartCoroutine(CurrencyScreenManager.instance.SetUpCurrencyScreen());
            GameStatusManager.SetGameEvent(TriggerOptions.RoundEnd);
            GameStatusManager._Status.handPlayedData.playedHandsInRound = new System.Collections.Generic.List<HandType>();
        }
        else
        {
            if (HandManager.instance.GetHandsRemaining() == 0)
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
