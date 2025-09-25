using UnityEngine;
[CreateAssetMenu(fileName = "Require Match Saved",menuName = "Scriptables/Joker/Trigger/Require Match Saved")]
public class MatchLossTrigger : JokerTrigger
{
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return ScoreManager.instance.saved;
    }
}
