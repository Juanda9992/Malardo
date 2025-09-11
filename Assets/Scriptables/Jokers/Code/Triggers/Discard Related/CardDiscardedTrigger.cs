using UnityEngine;
[CreateAssetMenu(fileName = "Required Card Discard",menuName = "Scriptables/Joker/Trigger/Required Card Discard")]
public class CardDiscardedTrigger : JokerTrigger
{
    public bool faceCards;
    public int cardNumber;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.discardData.lastDiscard.number == cardNumber)
        {
            return true;
        }
        return false;
    }
}
