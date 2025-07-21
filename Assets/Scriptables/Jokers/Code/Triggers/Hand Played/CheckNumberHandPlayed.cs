using UnityEngine;
[CreateAssetMenu(fileName = "Required Hand Played X number",menuName = "Scriptables/Joker/Trigger/Check if Hand played X times")]
public class CheckNumberHandPlayed : JokerTrigger
{
    public HandType[] checkForHand;
    public bool checkInRound;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.handPlayedData.CheckNumberHandPlayed(checkForHand, checkInRound) > 0;
    }
}
