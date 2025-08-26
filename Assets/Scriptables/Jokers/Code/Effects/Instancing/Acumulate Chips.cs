using UnityEngine;

[CreateAssetMenu(fileName = "Acumulate Chips",menuName = "Scriptables/Joker/Effect/Instance Effects/Acumulate Chips")]
public class AcumulateChips : JokerEffect
{
    public int chipsAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalChips += chipsAmmount;
        UpdateDescription(instance);
        instance.triggerMessage = "+" + chipsAmmount.ToString();
        instance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalChips.ToString());
    }
}
