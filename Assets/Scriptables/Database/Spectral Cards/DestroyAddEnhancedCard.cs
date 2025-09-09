using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Destroy Card Effect", menuName = "Scriptables/Tarot Card/Effect/Destroy and Create Card")]
public class DestroyAddEnhancedCard : CardEffect
{
    public int cardsToDestroy;
    public int cardsToCreate;

    public bool faceCards;

    public override void ApplyEffect()
    {
        DeckManager.instance.StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence()
    {
        for (int i = 0; i < cardsToDestroy; i++)
        {
            int randomIndex = Random.Range(0, CardManager.instance.cardsOnScreen.Count);
            CardManager.instance.cardsOnScreen[randomIndex].currentCard.linkedCard.pointerInteraction.DestroyCard();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);
        if (cardsToCreate > 0)
        {
            CardType[] cardTypes = new CardType[] { CardType.Stone, CardType.Gold, CardType.Bonus, CardType.Silver, CardType.Lucky, CardType.Glass, CardType.Mult };
            if (faceCards)
            {
                for (int i = 0; i < cardsToCreate; i++)
                {
                    Card card = new Card();
                    card.number = Random.Range(11, 14);
                    card.cardType = cardTypes[Random.Range(0, cardTypes.Length)];
                    card.SetCardChipAmmount();
                    card.SetCardName();
                    DeckManager.instance.AddCardOnFullDeck(card);

                    if (PackManager.instance.isOnPackMenu)
                    {

                    }
                    else
                    {
                        CardManager.instance.GenerateCardOnHand(card);
                    }

                    yield return new WaitForSeconds(0.15f);
                }
            }

        }
    }
}
