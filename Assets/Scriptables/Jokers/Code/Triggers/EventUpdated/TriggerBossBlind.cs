using UnityEngine;

[CreateAssetMenu(fileName = "Boss Blind Active",menuName = "Scriptables/Joker/Trigger/Require Boss Blind")]
public class TriggerBossBlind : JokerTrigger
{
    public bool active;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (active)
        {
            return BlindManager.instance.activeBossBlind != null;
        }
        else
        {
            return BlindManager.instance.activeBossBlind == null;
        }
    }
}
