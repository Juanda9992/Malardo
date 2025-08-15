using UnityEngine;
[CreateAssetMenu(fileName = "Required Hand Played X number",menuName = "Scriptables/Joker/Trigger/Check if Hand played X times")]
public class CheckNumberHandPlayed : JokerTrigger
{
    public bool checkInRound;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.currentGameStatus == TriggerOptions.None) return false;
        return PokerHandLevelStorage.instance.GetHandData(HandDetector.instance.currentHand.pokerHand.handType).playedInRound ;
    }
}
