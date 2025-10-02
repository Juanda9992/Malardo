using UnityEngine;

[CreateAssetMenu(menuName= "Scriptables/Tag/Effect/ Currency Per Statistic",fileName = "Currency Per Statistic")]
public class CurrencyPerStatistic : CardEffect
{
    public bool hand, discard;

    public override void ApplyEffect()
    {
        CurrencyManager.instance.AddCurrency(GetValue());
    }

    private int GetValue()
    {
        int value = 0;
        if (hand)
        {
            value = GameStatusManager._Status.handsUsed;
        }
        else if (discard)
        {
            value = GameStatusManager._Status.discardData.discardsUsed;
        }

        return value;
    }

    public override string GetDescription(string baseDescription)
    {
        return baseDescription.Replace("_R_", GetValue().ToString());
    }
}
