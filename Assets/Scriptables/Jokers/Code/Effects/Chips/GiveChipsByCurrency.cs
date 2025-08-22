using UnityEngine;
[CreateAssetMenu(fileName = "Give Chips By Currency",menuName = "Scriptables/Joker/Effect/Give Chips by Currency")]
public class GiveChipsByCurrency : JokerEffect
{
    public int chipAmmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(Calculate());
        jokerInstance.triggerMessage = $"+{Calculate()}";
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", Calculate().ToString());
    }

    private int Calculate()
    {
        return chipAmmount * CurrencyManager.instance.currentCurrency;
    }
}
