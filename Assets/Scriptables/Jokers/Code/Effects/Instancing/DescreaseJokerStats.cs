using UnityEngine;
[CreateAssetMenu(fileName ="Decrease Stats",menuName ="Scriptables/Joker/Effect/Instance Effects/Decrease Stats")]
public class DescreaseJokerStats : JokerEffect
{
    public int initialAmmount;
    public int decreaseRate;
    public bool chip;
    public bool autoDestroy;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.triggerMessage = "-" + decreaseRate.ToString();
        instance.jokerContainer.TriggerMessage();

        instance.totalChips -= decreaseRate;
        UpdateDescription(instance);
        if (autoDestroy)
        {
            if (chip)
            {
                instance.destroyJoker = instance.totalChips == 0;
            }       
        }
    }
    public override void SetupEffect(JokerInstance jokerInstance)
    {
        if (chip)
        {
            jokerInstance.totalChips = initialAmmount;
        }
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalChips.ToString());
    }
}
