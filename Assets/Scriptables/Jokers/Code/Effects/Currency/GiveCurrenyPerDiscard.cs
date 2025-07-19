using UnityEngine;

[CreateAssetMenu(fileName = "Give Currency per Discard", menuName = "Scriptables/Joker/Effect/Currency/Give Curerncy per Discard")]
public class GiveCurrenyPerDiscard : JokerEffect
{
    public override void ApplyEffect()
    {
        CurrencyManager.instance.AddCurrency((int)ammount * GameStatusManager._Status.discardsRemaining);
    }

    public override string GetCustomMessage()
    {
        return "$" + ((int)ammount * GameStatusManager._Status.discardsRemaining).ToString();
    }
}
