using UnityEngine;

[CreateAssetMenu(fileName = "Boss Blind Active",menuName = "Scriptables/Joker/Trigger/Require Boss Blind")]
public class TriggerBossBlind : JokerTrigger
{
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return BlindManager.instance.activeBossBlind != null;
    }
}
