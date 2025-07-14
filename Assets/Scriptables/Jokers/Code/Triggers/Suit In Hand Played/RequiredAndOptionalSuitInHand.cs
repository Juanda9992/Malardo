using UnityEngine;
[CreateAssetMenu(fileName = "Required And optional Suit",menuName = "Scriptables/Joker/Trigger/Required and Optional Suits")]
public class RequiredAndOptionalSuitInHand : JokerTrigger
{
    public Suit requiredSuit;

    public Suit[] optionalSuits;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        if (gameStatus.cardPlayed == null) return false;

        if (CardPlayer.instance.currentHand.Find(x => x.cardSuit == requiredSuit) == null)
        {
            return false;
        }

        for (int i = 0; i < optionalSuits.Length; i++)
        {
            if (CardPlayer.instance.currentHand.Find(x => x.cardSuit == optionalSuits[i]) != null)
            {
                return true;
            }
        }

        return true;
    }
}
