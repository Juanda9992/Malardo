using UnityEngine;
[CreateAssetMenu(fileName = "Required Discard Size",menuName = "Scriptables/Joker/Trigger/Required Discard Size")]
public class RequiredDiscardSize : JokerTrigger
{
    public int discardSizeRequired;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.discardData.discardSize == discardSizeRequired;
    }
}
