using UnityEngine;

[CreateAssetMenu(fileName = "Required Discards", menuName = "Scriptables/Joker/Trigger/Discard Ammount Trigger" )]
public class DiscardAmmountTrigger : JokerTrigger
{
    public int discardRequired;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.discardsRemaining == discardRequired;
    }
}
