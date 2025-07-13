using UnityEngine;
public abstract class JokerTrigger : ScriptableObject
{
    public abstract bool MeetCondition(GameStatus gameStatus);
}