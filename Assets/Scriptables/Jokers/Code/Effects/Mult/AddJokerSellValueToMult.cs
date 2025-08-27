using UnityEngine;
[CreateAssetMenu(fileName = "Add jokers sell value to mult", menuName = "Scriptables/Joker/Effect/Mult/Add jokers sell value to Mult")]
public class AddJokerSellValueToMult : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(JokerManager.instance.GetSellValueFromAllJokers());
        jokerInstance.triggerMessage = "+" + JokerManager.instance.GetSellValueFromAllJokers().ToString();
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", jokerInstance.triggerMessage);
        jokerInstance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.triggerMessage);
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        UpdateDescription(jokerInstance);
    }
}
