using UnityEngine;
[CreateAssetMenu(fileName = "One Discard Per Round",menuName = "Scriptables/Joker/Trigger/Control/One Discard Per Round")]
public class OncePerRound : JokerTrigger
{
    public bool discard;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (discard)
        {
            return gameStatus.discardData.discardsOnMatch == 1;
        }
        else
        {
            return gameStatus.firstHand;
        }
    }
}
