using UnityEngine;

[CreateAssetMenu(fileName = "Add 4 Chips", menuName = "Scriptables/Add 4 Chips")]
public class Add4ChipsOnPlay : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalChips += 4;
        UpdateDescription(instance);
        instance.triggerMessage = "+4";
        instance.jokerContainer.TriggerMessage();
    }
    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalChips.ToString());
    }
}
