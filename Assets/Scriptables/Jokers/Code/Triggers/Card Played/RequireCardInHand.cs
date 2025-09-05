using UnityEngine;
[CreateAssetMenu(fileName = "RequireCard",menuName = "Scriptables/Joker/Trigger/Require Card in Hand")]
public class RequireCardInHand : JokerTrigger
{
    public int cardNumber;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return HandManager.instance.handCards.Find(x => x.number == cardNumber) != null;
    }
}
