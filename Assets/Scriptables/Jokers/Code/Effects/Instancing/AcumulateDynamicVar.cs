using UnityEngine;
[CreateAssetMenu(fileName =  "Add Dynamic Var",menuName = "Scriptables/Joker/Effect/Instance Effects/Increase Dynamic Var")]
public class AcumulateDynamicVar : JokerEffect
{
    public int defaultVar;
    public int increaseAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.dynamicVariable += increaseAmmount;
        instance.triggerMessage = "+" + increaseAmmount;
        instance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_",instance.dynamicVariable.ToString());
    }
    public override void SetupEffect(JokerInstance jokerInstance)
    {
        jokerInstance.dynamicVariable = defaultVar;
        UpdateDescription(jokerInstance);
    }
}
