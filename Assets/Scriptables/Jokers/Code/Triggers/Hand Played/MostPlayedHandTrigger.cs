using UnityEngine;
[CreateAssetMenu(fileName = "Require Most Played Poker Hand", menuName = "Scriptables/Joker/Trigger/Most played hand")]
public class MostPlayedHandTrigger : JokerTrigger
{
    public bool required;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        return CommonOperations.CheckIfMostPlayedHand() == required;
    }
}
