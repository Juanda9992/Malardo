
using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult by Currency", menuName = "Scriptables/Joker/Effect/Give Mult by Currency")]
public class GiveMultByCurrency : JokerEffect
{
    public int divider;
    public int multValue;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(Calculate());

        jokerInstance.triggerMessage = "+" + Calculate();
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", Calculate().ToString());
        jokerInstance.triggerMessage = "+" + Calculate();
        jokerInstance.jokerContainer.TriggerMessage();
    }

    private float Calculate()
    {
        return Mathf.Ceil(CurrencyManager.instance.currentCurrency / divider);
    }
}
