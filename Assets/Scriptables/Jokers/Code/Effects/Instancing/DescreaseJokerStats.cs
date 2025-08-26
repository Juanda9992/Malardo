using UnityEngine;
[CreateAssetMenu(fileName = "Decrease Stats", menuName = "Scriptables/Joker/Effect/Instance Effects/Decrease Stats")]
public class DescreaseJokerStats : JokerEffect
{
    public int initialAmmount;
    public int decreaseRate;
    public bool chip;
    public bool mult;
    public bool autoDestroy;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.triggerMessage = "-" + decreaseRate.ToString();
        instance.jokerContainer.TriggerMessage();

        if (chip)
        {
            instance.totalChips -= decreaseRate;
        }
        if (mult)
        {
            instance.totalMult -= decreaseRate;
        }
        UpdateDescription(instance);
        if (autoDestroy)
        {
            if (chip)
            {
                instance.destroyJoker = instance.totalChips == 0;
            }
            if (mult)
            {
                instance.destroyJoker = instance.totalMult == 0;
            }
        }
    }
    public override void SetupEffect(JokerInstance jokerInstance)
    {
        if (chip)
        {
            jokerInstance.totalChips = initialAmmount;
        }
        if (mult)
        {
            jokerInstance.totalMult = initialAmmount;
        }
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        if (mult)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMult.ToString());
        }
        if (chip)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalChips.ToString());
        }
    }
}
