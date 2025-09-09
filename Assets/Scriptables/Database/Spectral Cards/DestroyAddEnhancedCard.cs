using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Destroy Card Effect", menuName = "Scriptables/Tarot Card/Effect/Destroy and Create Card")]
public class DestroyAddEnhancedCard : CardEffect
{
    public int cardsToDestroy;
    public int cardsToCreate;

    public bool numberedCards;
    public bool faceCards;
    public bool aces;
    public bool earnMoney;
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
            for (int i = 0; i < cardsToCreate; i++)
            {
                Card card = new Card();
                card.number = GetCardNumber();
                card.cardType = cardTypes[Random.Range(0, cardTypes.Length)];
                card.cardSuit = CommonOperations.GetRandomSuit();
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

        if (earnMoney)
        {
            CurrencyManager.instance.AddCurrency(20);
        }
    }

    private int GetCardNumber()
    {
        if (faceCards)
        {
            return Random.Range(11, 14);
        }
        if (aces)
        {
            return 1;
        }
        if (numberedCards)
        {
            return Random.Range(2, 11);
        }

        return 0;
    }
}
