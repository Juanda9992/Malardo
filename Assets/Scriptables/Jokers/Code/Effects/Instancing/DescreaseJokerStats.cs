using UnityEngine;
[CreateAssetMenu(fileName = "Decrease Stats", menuName = "Scriptables/Joker/Effect/Instance Effects/Decrease Stats")]
public class DescreaseJokerStats : JokerEffect
{
    public int initialAmmount;
    public float decreaseRate;
    public bool chip;
    public bool mult;
    public bool multiplier;
    public bool dynamicVariable;
    public bool autoDestroy;
    public override void ApplyEffect(JokerInstance instance)
    {
        instance.triggerMessage = "-" + decreaseRate.ToString();
        instance.jokerContainer.TriggerMessage();

        if (chip)
        {
            instance.totalChips -= (int)decreaseRate;
        }
        if (mult)
        {
            instance.totalMult -= decreaseRate;
        }
        if (dynamicVariable)
        {
            instance.dynamicVariable -= (int)decreaseRate;
        }
        if (multiplier)
        {
            instance.totalMultiplier -= decreaseRate;
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
            if (dynamicVariable)
            {
                instance.destroyJoker = instance.dynamicVariable == 0;
            }
            if (multiplier)
            {
                instance.destroyJoker = instance.totalMultiplier-1 <= 0;
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

        if (dynamicVariable)
        {
            jokerInstance.dynamicVariable = initialAmmount;
        }

        if (multiplier)
        {
            jokerInstance.totalMultiplier = initialAmmount;
        }

        UpdateDescription(jokerInstance);
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
        if (dynamicVariable)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.dynamicVariable.ToString());
        }

        if (multiplier)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMultiplier.ToString());
        }
    }
}
