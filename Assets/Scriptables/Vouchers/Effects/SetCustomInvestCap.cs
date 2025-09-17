using UnityEngine;
[CreateAssetMenu(fileName = "Set Currency Cap",menuName = "Scriptables/Voucher/Effect/Set Currency Cap")]
public class SetCustomInvestCap : CardEffect
{
    public int newCap;

    public override void ApplyEffect()
    {
        CurrencyScreenManager.instance.investCap = newCap;
    }
}
