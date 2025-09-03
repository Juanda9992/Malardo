using System.Collections;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(fileName = "Change to suit", menuName = "Scriptables/Tarot Card/Effect/Change Suit Effect")]
public class SuitEffects : CardEffect
{
    public Suit desiredSuit;
    public CardType cardType;
    public bool increaseRank;
    public bool destroyCards;
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
                HandManager.instance.handCards[i].linkedCard.visuals.IncreaseCardRankCoroutineRequest(cardType);
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
    }
    public override bool CanBeUsed()
    {
        return HandManager.instance.handCards.Count >= minCardsRequired && HandManager.instance.handCards.Count <= maxCardsRequired;
    }
}

