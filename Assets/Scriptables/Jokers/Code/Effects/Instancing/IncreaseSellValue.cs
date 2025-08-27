using UnityEngine;
[CreateAssetMenu(fileName = "Increase Sell Value",menuName = "Scriptables/Joker/Effect/Instance Effects/Increase Sell Value")]
public class IncreaseSellValue : JokerEffect
{
    public int increaseAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.extraSellValue += increaseAmmount;
        instance.triggerMessage = "Increase";
        instance.jokerContainer.TriggerMessage();
    }
}
