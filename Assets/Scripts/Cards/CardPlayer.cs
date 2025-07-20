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
    }
    public void SetHandPlayed(HandType handData)
    {
        lastHandPlayed = handData;
    }

    private IEnumerator PlayCards()
    {
        isPlayingCards = true;
        RemovePlayedCardsFromList();
        yield return new WaitForSeconds(0.1f);
        yield return TriggerHandCards();
        yield return new WaitForSeconds(0.2f);

        GameEventsManager.instance.TriggerSpecificandPlayed(lastHandPlayed);

        yield return CardManager.instance.ActivateCardHabilities();

        GameStatusManager._Status.currentGameStatus = TriggerOptions.HandEnd;
        GameEventsManager.instance.TriggerHandEnd();
        yield return JokerManager.instance.PlayJokersEndMatch();
        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.5f);
        HandManager.instance.ClearHandPlayed();
        currentHand.Clear();
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();
        isPlayingCards = false;
    }

    private IEnumerator TriggerHandCards()
    {
        for (int i = 0; i < currentHand.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                yield return PlayCard(currentHand[i]);
            }

            if (currentHand[i].cardSeal == Seal.Red)
            {
                ScoreSign.instance.SetMessage(Color.green, "Again!", currentHand[i].linkedCard.transform.position);
                yield return new WaitForSeconds(0.3f);
                yield return PlayCard(currentHand[i]);
            }
            yield return new WaitForSeconds(0.2f);
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

        if (card.cardSeal == Seal.Gold)
        {
            CurrencyManager.instance.AddCurrency(4);
            ScoreSign.instance.SetMessage(Color.yellow, "$4", card.linkedCard.transform.position);
            yield return new WaitForSeconds(0.3f);
        }

        switch (card.cardEdition)
        {
            case CardEdition.Base:
                break;
            case CardEdition.Foil:
                ScoreManager.instance.AddChips(50);
                ScoreSign.instance.SetMessage(Color.blue, "+50", card.linkedCard.transform.position);
                yield return new WaitForSeconds(0.4f);
                break;
            case CardEdition.Holographic:
                ScoreManager.instance.AddMult(10);
                ScoreSign.instance.SetMessage(Color.red, "+10", card.linkedCard.transform.position);
                yield return new WaitForSeconds(0.4f);
                break;
            case CardEdition.Polychrome:
                ScoreManager.instance.MultiplyMulti(1.5f);
                ScoreSign.instance.SetMessage(Color.red, "X1.5", card.linkedCard.transform.position);
                yield return new WaitForSeconds(0.3f);
                break;

        }


    }

    private void RemovePlayedCardsFromList()
    {
        GameStatusManager.SetHandSize(currentHand.Count);

        for (int i = 0; i < currentHand.Count; i++)
        {
            CardManager.instance.cardsOnScreen.Remove(currentHand[i].linkedCard);
        }
    }
}
