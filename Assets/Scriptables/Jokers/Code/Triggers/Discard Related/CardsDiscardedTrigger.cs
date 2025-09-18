using UnityEngine;
[CreateAssetMenu(fileName = "Required Cards Discarded",menuName = "Scriptables/Joker/Trigger/Cards Discarded")]
public class CardsDiscardedTrigger : JokerTrigger
{
    public int cardsRequired;
    public bool faceCards;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (faceCards)
        {
            return gameStatus.discardData.discardCards.FindAll(x => x.IsFaceCard).Count >= cardsRequired;
        }
        return false;
    }
}
