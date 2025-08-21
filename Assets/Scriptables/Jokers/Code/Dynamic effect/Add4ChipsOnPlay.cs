using UnityEngine;

[CreateAssetMenu(fileName = "Add 4 Chips", menuName = "Scriptables/Add 4 Chips")]
public class Add4ChipsOnPlay : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.totalChips += 4;
        UpdateDescription(instance);
    }
    public override void UpdateDescription(JokerInstance instance)
    {
        string Template = instance.data.description;
        instance.jokerDescription = Template.Replace("_R_", instance.totalChips.ToString());
        instance.triggerMessage = "+" +instance.totalChips.ToString();
    }
}
