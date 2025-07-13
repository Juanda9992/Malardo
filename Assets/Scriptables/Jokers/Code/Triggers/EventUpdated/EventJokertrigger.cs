using UnityEngine;

[CreateAssetMenu (fileName = "Event Trigger",menuName = "Scriptables/Joker/Trigger/Event Trigger")]
public class EventJokertrigger : JokerTrigger
{
    public TriggerOptions triggerOption;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.currentGameStatus == triggerOption;
    }
}
