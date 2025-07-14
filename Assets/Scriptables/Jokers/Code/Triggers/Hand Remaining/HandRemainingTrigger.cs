
using UnityEngine;
[CreateAssetMenu(fileName = "Required Hands",menuName = "Scriptables/Joker/Trigger/Required Hands Remainings")]
public class HandRemainingTrigger : JokerTrigger
{
    public int minAmmount, maxAmmount;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.handsRemaining >= minAmmount && gameStatus.handsRemaining <= maxAmmount;
    }
}
