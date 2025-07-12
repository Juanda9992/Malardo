using UnityEngine;

[CreateAssetMenu(fileName = "Suit", menuName = "Scriptables/Joker/Trigger/Suit Trigger")]
public class SuitPlayedTrigger : JokerTrigger
{
    public Suit requiredSuit;
    public override void TriggerEffect()
    {

    }
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return gameStatus.cardPlayed.cardSuit == requiredSuit;
    }
}
