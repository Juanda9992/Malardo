using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Required Suits in Played Hand", menuName = "Scriptables/Joker/Trigger/Require Suits in Hand")]
public class RequiredSuitsInHandPlayed : JokerTrigger
{
    public int detectionsRequired;
    public List<Suit> requiredSuits;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.cardPlayed == null) return false;

        for (int i = 0; i < requiredSuits.Count;i++)
        {
            if (CardPlayer.instance.currentHand.Find(x => x.cardSuit == requiredSuits[i]) == null)
            {
                return false;
            }
        }

        return true;
    }
}
