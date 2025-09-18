using UnityEngine;

[CreateAssetMenu(fileName = "Suit", menuName = "Scriptables/Joker/Trigger/Suit Trigger")]
public class SuitPlayedTrigger : JokerTrigger
{
    public Suit requiredSuit;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.cardPlayed == null)
        {
            return false;
        }

        if (gameStatus.cardPlayed.cardType == CardType.Stone)
        {
            return false;
        } 

        return gameStatus.cardPlayed.cardSuit == requiredSuit || gameStatus.cardPlayed.cardType == CardType.Wild;
        
    }
}
