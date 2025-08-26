using UnityEngine;
[CreateAssetMenu(fileName = "MultiplyMultXRounds",menuName = "Scriptables/Joker/Effect/Instance Effects/Multiply Mult X Rounds")]
public class MultiplyMultEveryXRounds : JokerEffect
{
    [SerializeField] private int startingRounds;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (instance.dynamicVariable == 0)
        {
            ScoreManager.instance.MultiplyMulti(4);
            instance.triggerMessage = "X4";
            instance.jokerContainer.TriggerMessage();
        }
        instance.dynamicVariable--;

        instance.dynamicVariable = instance.dynamicVariable < 0 ? startingRounds : instance.dynamicVariable;
        instance.triggerMessage = "";

        UpdateDescription(instance);
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        if (instance.dynamicVariable != 0)
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", instance.dynamicVariable.ToString());
        }
        else
        {
            instance.jokerDescription = instance.data.description.Replace("_R_", "ACTIVE");
        }
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        jokerInstance.dynamicVariable = startingRounds;
        UpdateDescription(jokerInstance);
    }
}
