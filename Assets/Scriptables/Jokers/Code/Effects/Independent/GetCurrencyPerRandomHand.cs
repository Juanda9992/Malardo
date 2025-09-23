using UnityEngine;
[CreateAssetMenu(fileName = "Add currency per played random hand", menuName = "Scriptables/Joker/Effect/Independent/Currency per Random Hand")]
public class GetCurrencyPerRandomHand : JokerEffect
{
    public int currency;
    public bool handPlayed;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (handPlayed)
        {
            CurrencyManager.instance.AddCurrency(currency);
            instance.triggerMessage = "$" + currency;
            instance.jokerContainer.TriggerMessage();
        }
    }

    public override bool Scores(JokerInstance instance)
    {
        Debug.Log(instance.randomHand + " " + GameStatusManager._Status.playedHand);
        return GameStatusManager._Status.playedHand == instance.randomHand;
    }
}
