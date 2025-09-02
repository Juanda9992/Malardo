using UnityEngine;
[CreateAssetMenu(fileName = "Currency Effect",menuName = "Scriptables/Tarot Card/Effect/Currency Effect")]
public class CurrencyEffects : CardEffect
{
    public bool duplicateMoney;
    public bool sellValueToMoney;
    public override void ApplyEffect()
    {
        if (duplicateMoney)
        {
            int ammount = CurrencyManager.instance.currentCurrency;
            ammount = Mathf.Clamp(ammount, 0, 20);
            CurrencyManager.instance.AddCurrency(ammount);
        }
        if (sellValueToMoney)
        {
            int ammount = JokerManager.instance.GetSellValueFromAllJokers();
            ammount = Mathf.Clamp(ammount, 0, 50);
            CurrencyManager.instance.AddCurrency(ammount);
        }
    }
}
