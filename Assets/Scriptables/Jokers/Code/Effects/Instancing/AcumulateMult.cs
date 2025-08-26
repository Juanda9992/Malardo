using UnityEngine;

[CreateAssetMenu(fileName = "Acumulate Mult", menuName = "Scriptables/Joker/Effect/Instance Effects/Acumulate Mult")]
public class AcumulateMult : JokerEffect
{
    public int multAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalMult += multAmmount;
        UpdateDescription(instance);
        instance.triggerMessage = "+" + multAmmount.ToString();
        instance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMult.ToString());
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        UpdateDescription(jokerInstance);
    }


}
