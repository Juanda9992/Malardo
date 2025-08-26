using UnityEngine;
[CreateAssetMenu(fileName = "Reset Stats",menuName = "Scriptables/Joker/Effect/Instance Effects/ Reset Stats")]
public class ResetJokerInstanceStats : JokerEffect
{
    public bool mult;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (mult)
        {
            instance.totalMult = 0;

        }

        instance.triggerMessage = "Reset!";
        instance.jokerContainer.TriggerMessage();
    }
}
