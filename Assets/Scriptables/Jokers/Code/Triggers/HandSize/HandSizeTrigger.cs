using UnityEngine;

[CreateAssetMenu(fileName = "Hand Size", menuName = "Scriptables/Joker/Trigger/Hand Size Trigger")]
public class HandSizeTrigger : JokerTrigger
{
    public int minRequiredSize, maxRequiredSize;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.handSize <= maxRequiredSize && gameStatus.handSize >= minRequiredSize;
    }
}
