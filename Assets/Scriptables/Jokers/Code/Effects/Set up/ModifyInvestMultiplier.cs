using UnityEngine;
[CreateAssetMenu(fileName = "Modify Invest Multiplier",menuName = "Scriptables/Joker/Effect/Set up/Modify Invest Multiplier")]
public class ModifyInvestMultiplier : JokerEffect
{
    public bool increase;

    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        if (increase)
        {
            CurrencyScreenManager.instance.investMultiplier++;
        }
        else
        {
            CurrencyScreenManager.instance.investMultiplier--;
        }
    }
}
