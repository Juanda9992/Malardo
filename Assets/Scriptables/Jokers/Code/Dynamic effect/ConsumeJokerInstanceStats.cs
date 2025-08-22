using UnityEngine;
[CreateAssetMenu(fileName = "ConsumeJokerStats", menuName = "Scriptables/Joker/Effect/Instance Effects/Consume Joker Stats")]
public class ConsumeJokerInstanceStats : JokerEffect
{
    public bool chips;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (chips)
        {
            ScoreManager.instance.AddChips(instance.totalChips);
            instance.triggerMessage = "+" + instance.totalChips;
        }

        instance.jokerContainer.TriggerMessage();
    }
}
