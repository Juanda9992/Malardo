using UnityEngine;
[CreateAssetMenu(fileName = "Set Max Debt",menuName ="Scriptables/Joker/Effect/Set up/Set Max Debt")]
public class SetMaxDebtEffect : JokerEffect
{
    public int debtAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        CurrencyManager.instance.maxDebt = debtAmmount;
    }
}
