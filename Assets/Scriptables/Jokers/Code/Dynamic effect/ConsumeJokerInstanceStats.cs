using UnityEngine;
[CreateAssetMenu(fileName = "ConsumeJokerStats", menuName = "Scriptables/Joker/Effect/Instance Effects/Consume Joker Stats")]
public class ConsumeJokerInstanceStats : JokerEffect
{
    public bool chips;
    public bool mult;
    public bool multiplier;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (chips)
        {
            ScoreManager.instance.AddChips(instance.totalChips);
            instance.triggerMessage = "+" + instance.totalChips;
        }

        if (mult)
        {
            ScoreManager.instance.AddMult(instance.totalMult);
            instance.triggerMessage = "+" + instance.totalMult;
        }

        if (multiplier)
        {
            ScoreManager.instance.MultiplyMulti(instance.totalMultiplier);
            instance.triggerMessage = "x" + instance.totalMultiplier;
        }

        instance.jokerContainer.TriggerMessage();
    }
}
