using UnityEngine;
[CreateAssetMenu(fileName = "Required Number of Special Cards",menuName = "Scriptables/Joker/Trigger/Require Special Cards on Deck")]
public class RequireSpecialCardsOnDeck : JokerTrigger
{
    public int requireAmmount;
    public override bool MeetCondition(GameStatus gameStatus)
    {
        return requireAmmount <= DeckManager.instance.GetNumberOfSpecialCardsInDeck();
    }
}
