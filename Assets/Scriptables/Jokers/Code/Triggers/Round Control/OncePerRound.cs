using UnityEngine;
[CreateAssetMenu(fileName = "One Discard Per Round",menuName = "Scriptables/Joker/Trigger/Control/One Discard Per Round")]
public class OncePerRound : JokerTrigger
{
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.discardData.discardsOnMatch == 1;
    }
}
