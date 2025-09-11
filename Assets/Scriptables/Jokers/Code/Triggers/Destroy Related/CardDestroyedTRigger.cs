using UnityEngine;
[CreateAssetMenu(fileName = "Card Destroyed Trigger",menuName = "Scriptables/Joker/Trigger/Card Destroyed")]
public class CardDestroyedTRigger : JokerTrigger
{
    public bool faceCard;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (faceCard)
        {
            if (gameStatus.destroyedCard.IsFaceCard)
            {
                return true;
            }
        }

        return false;
    }
}
