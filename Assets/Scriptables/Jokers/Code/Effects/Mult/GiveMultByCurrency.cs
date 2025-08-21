
using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult by Currency", menuName = "Scriptables/Joker/Effect/Give Mult by Currency")]
public class GiveMultByCurrency : JokerEffect
{
    public int divider;
    public int multValue;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(Mathf.Ceil(CurrencyManager.instance.currentCurrency / divider));
    }

    public override string GetCustomMessage()
    {
        return (Mathf.Ceil(CurrencyManager.instance.currentCurrency / divider) * multValue).ToString();
    }
}
