using UnityEngine;
[CreateAssetMenu(fileName = "Add currency per played random hand", menuName = "Scriptables/Joker/Effect/Independent/Currency per Random Hand")]
public class GetCurrencyPerRandomHand : JokerEffect
{
    public int currency;
    public bool handPlayed;
    public bool numberDiscarded;
    public override void ApplyEffect(JokerInstance instance)
    {
        CurrencyManager.instance.AddCurrency(currency);
        instance.triggerMessage = "$" + currency;
        instance.jokerContainer.TriggerMessage();
    }

    public override bool Scores(JokerInstance instance)
    {
        if (handPlayed)
        {
            return GameStatusManager._Status.playedHand == instance.randomHand;
        }
        else
        {
            return GameStatusManager._Status.discardData.lastDiscard.number == instance.dynamicVariable;
        }
    }
}
