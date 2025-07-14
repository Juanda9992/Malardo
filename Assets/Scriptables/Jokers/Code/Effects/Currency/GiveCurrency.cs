using UnityEngine;
[CreateAssetMenu(fileName = "Give Currency", menuName = "Scriptables/Joker/Effect/Give Currency")]
public class GiveCurrency : JokerEffect
{
    public override void ApplyEffect()
    {
        CurrencyManager.instance.AddCurrency((int)ammount);
    }

    public override string GetCustomMessage()
    {
        return "$" + ammount;
    }
}
