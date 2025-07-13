using UnityEngine;

[CreateAssetMenu(fileName = "Required Card Number Trigger",menuName = "Scriptables/Joker/Trigger/Card Number Trigger")]
public class RequiredCardTrigger : JokerTrigger
{
    public int[] requiredCard;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.cardPlayed == null) return false;
        for (int i = 0; i < requiredCard.Length; i++)
        {
            if (requiredCard[i] == gameStatus.cardPlayed.number)
            {
                return true;
            }
        }

        return false;
    }
}
