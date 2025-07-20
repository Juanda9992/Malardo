using UnityEngine;
[CreateAssetMenu(fileName = "Require Card Type",menuName = "Scriptables/Joker/Trigger/Require Card Type")]
public class RequireCardTypeTrigger : JokerTrigger
{
    public CardType requiredType;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.cardPlayed == null) return false;

        return gameStatus.cardPlayed.cardType == requiredType;
    }
}
