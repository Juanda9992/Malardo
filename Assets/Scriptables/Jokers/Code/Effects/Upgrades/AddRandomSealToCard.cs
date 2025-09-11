using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Add Random Seal",menuName = "Scriptables/Joker/Effect/Misc/Add Random Seal to Card")]
public class AddRandomSealToCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        List<Card_Data> cards = CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.cardSeal == Seal.None);

        if (cards.Count > 0)
        {
            int randomCard = Random.Range(0, cards.Count);
            cards[randomCard].currentCard.cardSeal = CommonOperations.GetRandomSeal(false);
            cards[randomCard].visuals.SetVisuals(cards[randomCard].currentCard);
        }
    }
}
