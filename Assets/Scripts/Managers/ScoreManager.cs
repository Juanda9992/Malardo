using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public bool saved = false;

    [SerializeField] private float chips = 0;
    [SerializeField] private float mult = 1;
    [SerializeField] private float roundScore;

    [SerializeField] private TextMeshProUGUI scoreText, chipsText, multText, totalScoreText;
    void Awake()
    {
        instance = this;

        ResetChipsAndMult();
    }

    void Start()
    {
        BlindManager.instance.OnBlindDefeated += ResetRoundScore;
        scoreText.text = "0";
        totalScoreText.text = "";
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

    public void DivideChipsAndMult()
    {
        mult /= 2;
        chips /= 2;

        multText.text = mult.ToString("0.##");
        chipsText.text = chips.ToString();

    }

    public void CalculateScore()
    {
        float currentScore = chips * mult;

        roundScore += currentScore;

        totalScoreText.text = roundScore.ToString();
        scoreText.text = currentScore.ToString();
    }

    public bool CheckBlindDefeated()
    {
        return BlindManager.instance.BlindDefeated((int)roundScore);
    }

    [ContextMenu("Skip Match")]
    public void OnBlindDefeated()
    {
        if (EndGameManager.instance.ReachedEndGame())
        {
            EndGameManager.instance.SetEndGame();
            return;
        }
        BlindManager.instance.SetBlindDefeated();
        StartCoroutine(CurrencyScreenManager.instance.SetUpCurrencyScreen());
        GameStatusManager.SetGameEvent(TriggerOptions.RoundEnd);
        GameStatusManager._Status.handPlayedData.playedHandsInRound = new System.Collections.Generic.List<HandType>();
        totalScoreText.text = "0";
    }

    public IEnumerator TryEndMatch()
    {
        if (HandManager.instance.GetHandsRemaining() == 0)
        {
            float quarterScore = BlindManager.instance.requiredScore / 4;
            if (roundScore >= quarterScore && CardPlayer.instance.extraLives > 0)
            {
                saved = true;
                yield return JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnMatchLoss);
                yield return new WaitForSeconds(0.5f);
                CardPlayer.instance.NextMatch();
                saved = false;
                yield break;
            }
            LoseManager.instance.SetLoseScreen();
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
