using UnityEngine;

[CreateAssetMenu(fileName = "Hand Size", menuName = "Scriptables/Joker/Trigger/Hand Size Trigger")]
public class HandSizeTrigger : JokerTrigger
{
    public int minRequiredSize, maxRequiredSize;
    public bool playedCards;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (playedCards)
        {
            return gameStatus.handSize <= maxRequiredSize && gameStatus.handSize >= minRequiredSize;
        }
        else
        {
            Debug.Log(HandManager.instance.handCards.Count);
            if (HandManager.instance.handCards.Count >= minRequiredSize && HandManager.instance.handCards.Count <= minRequiredSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
