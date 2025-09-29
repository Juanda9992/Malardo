using UnityEngine;
[CreateAssetMenu(fileName = "Currency Effect", menuName = "Scriptables/Tarot Card/Effect/Currency Effect")]
public class CurrencyEffects : CardEffect
{
    public bool duplicateMoney;
    public int moneyLimit;
    public bool sellValueToMoney;
    public override void ApplyEffect()
    {
        if (duplicateMoney)
        {
            CurrencyManager.instance.AddCurrency(CardCalculation());
        }
        if (sellValueToMoney)
        {
            CurrencyManager.instance.AddCurrency(CardCalculation());
        }

        JokerManager.instance.StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnTarotCardUsed));
    }

    private int CardCalculation()
    {
        int ammount;
        if (duplicateMoney)
        {
            ammount = CurrencyManager.instance.currentCurrency;
            ammount = Mathf.Clamp(ammount, 0, moneyLimit);
        }
        else
        {
            ammount = JokerManager.instance.GetSellValueFromAllJokers();
            ammount = Mathf.Clamp(ammount, 0, moneyLimit);

        }
        return ammount;
    }

    public override string GetDescription(string baseDescription)
    {
        return baseDescription.Replace("_R_", CardCalculation().ToString());
    }
}
