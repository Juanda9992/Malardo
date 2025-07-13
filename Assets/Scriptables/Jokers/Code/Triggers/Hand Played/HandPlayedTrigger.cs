using UnityEngine;

[CreateAssetMenu(fileName = "Hand Trigger", menuName = "Scriptables/Joker/Trigger/Hand Related Trigger")]
public class HandPlayedTrigger : JokerTrigger
{
    public HandType[] requiredHandType;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        for (int i = 0; i < requiredHandType.Length; i++)
        {
            if (requiredHandType[i] == gameStatus.playedHand)
            {
                return true;
            }
        }
        return false;
    }
}
