using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public static CardPlayer instance;
    public bool isPlayingCards = false;
    public List<Card> currentHand;
    private HandType lastHandPlayed;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameEventsManager.instance.OnHandPlayed += () => StartCoroutine(nameof(PlayCards));
    }
    public void ReceiveHandCards(List<Card> cards)
    {
        currentHand = cards;

        currentHand = currentHand.OrderBy(x => x.linkedCard.transform.GetSiblingIndex()).ToList();
        GameStatusManager.SetHandSize(currentHand.Count);

        for (int i = 0; i < currentHand.Count; i++)
        {
            CardManager.instance.cardsOnScreen.Remove(currentHand[i].linkedCard);
        }
    }
    public void SetHandPlayed(HandType handData)
    {
        lastHandPlayed = handData;
    }

    private IEnumerator PlayCards()
    {
        isPlayingCards = true;
        yield return TriggerHandCards();
        yield return new WaitForSeconds(0.2f);

        GameEventsManager.instance.TriggerSpecificandPlayed(lastHandPlayed);

        yield return new WaitForSeconds(0.3f);

        Debug.Log("Hand end");
        GameStatusManager._Status.currentGameStatus = TriggerOptions.HandEnd;
        GameEventsManager.instance.TriggerHandEnd();
        yield return JokerManager.instance.PlayJokersEndMatch();

        yield return new WaitForSeconds(0.5f);
        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.3f);
        HandManager.instance.ClearHandPlayed();
        currentHand.Clear();

        yield return new WaitForSeconds(0.3f);
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();
        isPlayingCards = false;
    }

    private IEnumerator TriggerHandCards()
    {
        for (int i = 0; i < currentHand.Count; i++)
        {

            for (int j = 0; j < 1; j++)
            {
                yield return PlayCard(currentHand[i]);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private IEnumerator PlayCard(Card card)
    {
        //SCORE LOGIC
        ScoreManager.instance.AddChips(card.chipAmmount);
        ScoreSign.instance.SetScoreSign(card);


        //VISUAL LOGIC
        card.linkedCard.pointerInteraction.ShakeCard();

        yield return new WaitForSeconds(0.3f);
        //EVENTS
        GameEventsManager.instance.TriggerCardPlayed(card);
        GameStatusManager.SetLastCardPlayed(card);
    }
}
