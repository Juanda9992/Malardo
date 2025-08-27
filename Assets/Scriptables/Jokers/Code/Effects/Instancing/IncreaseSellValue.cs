using UnityEngine;
[CreateAssetMenu(fileName = "Increase Sell Value", menuName = "Scriptables/Joker/Effect/Instance Effects/Increase Sell Value")]
public class IncreaseSellValue : JokerEffect
{
    public int increaseAmmount;
    public bool self = true;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (!self)
        {
            foreach (var Joker in JokerManager.instance.currentJokers)
            {
                Joker._jokerInstance.extraSellValue += increaseAmmount;
            }
        }
        else
        {
            instance.extraSellValue += increaseAmmount;
        }

        instance.triggerMessage = "Increase";
        instance.jokerContainer.TriggerMessage();
    }
}
