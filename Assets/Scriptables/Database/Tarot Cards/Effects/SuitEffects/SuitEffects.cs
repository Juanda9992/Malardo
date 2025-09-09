using System.Collections;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(fileName = "Change to suit", menuName = "Scriptables/Tarot Card/Effect/Change Suit Effect")]
public class SuitEffects : CardEffect
{
    public Suit desiredSuit;
    public CardType cardType;
    public bool increaseRank;
    public bool copyCard;
    public bool destroyCards;
    public bool sameSuit;
    public bool sameNumber;
    public bool addRandomEdition;
    public int minCardsRequired, maxCardsRequired;
    public override void ApplyEffect()
    {
        DeckManager.instance.StartCoroutine(ChangeCards());
    }

    private IEnumerator ChangeCards()
    {
        if (desiredSuit != Suit.None)
        {
            for (int i = 0; i < HandManager.instance.handCards.Count; i++)
            {
                HandManager.instance.handCards[i].linkedCard.visuals.UpdateCardSuitCoroutineRequest(desiredSuit);
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (cardType != CardType.Default)
        {
            for (int i = 0; i < HandManager.instance.handCards.Count; i++)
            {
                HandManager.instance.handCards[i].linkedCard.visuals.UpdateCardTypeCoroutineRequest(cardType);
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (increaseRank)
        {
            for (int i = 0; i < HandManager.instance.handCards.Count; i++)
            {
                HandManager.instance.handCards[i].linkedCard.visuals.IncreaseCardRankCoroutineRequest();
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (destroyCards)
        {
            for (int i = 0; i < HandManager.instance.handCards.Count; i++)
            {
                HandManager.instance.handCards[i].linkedCard.pointerInteraction.DestroyCard();
                yield return new WaitForSeconds(0.1f);
            }
            HandManager.instance.handCards.Clear();
        }

        if (copyCard)
        {
            Card[] orderedCards = new Card[2];

            if (HandManager.instance.handCards[0].linkedCard.transform.position.x < HandManager.instance.handCards[1].linkedCard.transform.position.x)
            {
                orderedCards[0] = HandManager.instance.handCards[0];
                orderedCards[1] = HandManager.instance.handCards[1];
            }
            else
            {
                orderedCards[0] = HandManager.instance.handCards[1];
                orderedCards[1] = HandManager.instance.handCards[0];
            }

            Debug.Log(orderedCards[0].linkedCard.transform.position.x + " " + orderedCards[0].cardName);
            Debug.Log(orderedCards[1].linkedCard.transform.position.x + " " + orderedCards[1].cardName);

            orderedCards[0].linkedCard.visuals.CoptyCardCoroutineRequest(orderedCards[1]);
        }

        if (addRandomEdition)
        {
            HandManager.instance.handCards[0].cardEdition = CommonOperations.GetRandomCardEdition(false);
            HandManager.instance.handCards[0].linkedCard.visuals.SetVisuals(HandManager.instance.handCards[0]);
        }

        if (sameSuit)
        {
            Suit randomSuit = CommonOperations.GetRandomSuit();
            for (int i = 0; i < CardManager.instance.cardsOnScreen.Count; i++)
            {
                CardManager.instance.cardsOnScreen[i].visuals.UpdateCardSuitCoroutineRequest(randomSuit);
                yield return new WaitForSeconds(0.1f);
            }

            yield break;
        }
        if (sameNumber)
        {
            int randomNumber = Random.Range(1, 14);
            for (int i = 0; i < CardManager.instance.cardsOnScreen.Count; i++)
            {
                CardManager.instance.cardsOnScreen[i].visuals.UpdateNumberCoroutine(randomNumber);
                yield return new WaitForSeconds(0.1f);
            }

            DeckManager.instance.AddHandSize(-1);

            yield break;
        }

        JokerManager.instance.StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnTarotCardUsed));
    }
    public override bool CanBeUsed()
    {
        return HandManager.instance.handCards.Count >= minCardsRequired && HandManager.instance.handCards.Count <= maxCardsRequired;
    }
}

