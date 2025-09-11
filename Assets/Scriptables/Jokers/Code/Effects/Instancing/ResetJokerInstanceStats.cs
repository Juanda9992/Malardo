using UnityEngine;
[CreateAssetMenu(fileName = "Reset Stats", menuName = "Scriptables/Joker/Effect/Instance Effects/ Reset Stats")]
public class ResetJokerInstanceStats : JokerEffect
{
    public bool mult;
    public bool multiplier;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (mult)
        {
            instance.totalMult = 0;
        }
        if (multiplier)
        {
            instance.totalMultiplier = 1;
        }

        instance.triggerMessage = "Reset!";
        instance.jokerContainer.TriggerMessage();
        UpdateDescription(instance);
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        if (mult)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMult.ToString());
        }
        if (multiplier)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_",instance.totalMultiplier.ToString());
        }
    }
}
