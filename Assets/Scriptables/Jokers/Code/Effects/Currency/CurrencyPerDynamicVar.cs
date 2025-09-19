using UnityEngine;
[CreateAssetMenu(fileName = "Add currency Per Dynamic Var",menuName = "Scriptables/Joker/Effect/Instance Effects/Currency per Dynamic Var")]
public class CurrencyPerDynamicVar : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        CurrencyManager.instance.AddCurrency(instance.dynamicVariable);
        instance.triggerMessage = "$" + instance.dynamicVariable;
        instance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.dynamicVariable.ToString());
    }
}
