using UnityEngine;
[CreateAssetMenu(fileName = "Exclude Cards",menuName = "Scriptables/Joker/Trigger/Exclude Cards")]
public class ExcludeCards : JokerTrigger
{
    public bool faceCards;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (faceCards)
        {
            return CardPlayer.instance.currentHand.Find(x => x.IsFaceCard) == null;
        }

        return true;
    }
}
