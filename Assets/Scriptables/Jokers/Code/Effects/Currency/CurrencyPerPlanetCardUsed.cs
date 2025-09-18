using UnityEngine;
[CreateAssetMenu(fileName = "Currency Per planet Card",menuName = "Scriptables/Joker/Effect/Currency/Currency Per Planet Card")]
public class CurrencyPerPlanetCardUsed : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        CurrencyManager.instance.AddCurrency(Calculate());
        instance.triggerMessage = "+" + Calculate();
        instance.jokerContainer.TriggerMessage();
    }
    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", Calculate().ToString());
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        UpdateDescription(jokerInstance);
    }

    private int Calculate()
    {
        return PokerHandUpgrader.instance.usedCards.Count;
    }
}
