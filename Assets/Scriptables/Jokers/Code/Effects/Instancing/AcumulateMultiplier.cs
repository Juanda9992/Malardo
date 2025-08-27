using UnityEngine;
[CreateAssetMenu(fileName = "Acumulate Multiplier", menuName = "Scriptables/Joker/Effect/Instance Effects/Acumulate Multiplier")]
public class AcumulateMultiplier : JokerEffect
{
    public float multiplierAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalMultiplier += multiplierAmmount;
        instance.triggerMessage = "x" + multiplierAmmount.ToString();
        instance.jokerContainer.TriggerMessage();
        UpdateDescription(instance);
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMultiplier.ToString());
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        jokerInstance.totalMultiplier = 1;
        UpdateDescription(jokerInstance);
    }
}
