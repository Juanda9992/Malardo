using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public static CardPlayer instance;
    private List<Card> currentHand;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameEventsManager.instance.OnHandPlayed += ()=>StartCoroutine(nameof(PlayCards));
    }
    public void ReceiveHandCards(List<Card> cards)
    {
        currentHand = cards;
    }

    private IEnumerator PlayCards()
    {
        for (int i = 0; i < currentHand.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            ScoreManager.instance.AddChips(currentHand[i].number);
            ScoreSign.instance.SetScoreSign(currentHand[i]);
            currentHand[i].linkedCard.pointerInteraction.ShakeCard();
            GameEventsManager.instance.TriggerCardPlayed(currentHand[i]);
        }

        yield return new WaitForSeconds(0.5f);

        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.3f);

        HandManager.instance.DiscardAllCards();

        yield return new WaitForSeconds(0.3f);

        currentHand.Clear();
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();

    }
}
