using UnityEngine;
[CreateAssetMenu(fileName = "Lower Currency",menuName = "Scriptables/Joker/Trigger/Lower Currency")]
public class LowerCurrencyTrigger : JokerTrigger
{
    public int minimmumCurrency;

    public override bool MeetCondition(GameStatus gameStatus)
    {
        return CurrencyManager.instance.currentCurrency <= minimmumCurrency;
    }
}
