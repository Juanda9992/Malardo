using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Only Suit In Hand", menuName = "Scriptables/Joker/Trigger/Require Suits in Deck")]
public class SuitsOnHandTrigger : JokerTrigger
{
    public List<Suit> requiredSuits;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        foreach (var card in CardManager.instance.cardsOnScreen)
        {
            if (requiredSuits.IndexOf(card.currentCard.cardSuit) == -1)
            {
                return false;
            }
        }
        return true;

    }
}
