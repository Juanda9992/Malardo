using UnityEngine;
[CreateAssetMenu(fileName = "Give Chips By Currency",menuName = "Scriptables/Joker/Effect/Give Chips by Currency")]
public class GiveChipsByCurrency : JokerEffect
{
    public int chipAmmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(chipAmmount * CurrencyManager.instance.currentCurrency);
    }

    public override string GetCustomMessage()
    {
        return (chipAmmount * CurrencyManager.instance.currentCurrency).ToString();
    }
}
