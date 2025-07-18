using UnityEngine;
[CreateAssetMenu(fileName = "Required Discard Counter",menuName = "Scriptables/Joker/Trigger/Required Discard Counter")]
public class DiscardCounterTrigger : JokerTrigger
{
    public int counter;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.discardData.discardCount == counter;
    }
}
