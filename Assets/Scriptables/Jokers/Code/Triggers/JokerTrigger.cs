using UnityEngine;
public abstract class JokerTrigger : ScriptableObject
{
    public abstract void TriggerEffect();
    public abstract bool MeetCondition(GameStatus gameStatus);
}