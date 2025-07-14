using UnityEngine;

[CreateAssetMenu(fileName = "Random Trigger",menuName = "Scriptables/Joker/Trigger/Random Number Trigger")]
public class RandomNumberTrigger : JokerTrigger
{
    public int maxRange, requiredNumber;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        return Random.Range(0, maxRange) == requiredNumber;
    }
}
