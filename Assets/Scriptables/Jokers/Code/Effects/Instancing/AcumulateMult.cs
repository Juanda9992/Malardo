using UnityEngine;

[CreateAssetMenu(fileName = "Acumulate Mult", menuName = "Scriptables/Joker/Effect/Instance Effects/Acumulate Mult")]
public class AcumulateMult : JokerEffect
{
    public int multAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalMult += multAmmount;
        instance.totalMult = instance.totalMult < 0 ? 0 : instance.totalMult;
        instance.triggerMessage = (multAmmount > 0 ? "+" : "-") + Mathf.Abs(multAmmount).ToString();
        instance.jokerContainer.TriggerMessage();
        UpdateDescription(instance);

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
