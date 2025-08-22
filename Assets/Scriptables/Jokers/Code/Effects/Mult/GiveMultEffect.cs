using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult Effect", menuName = "Scriptables/Joker/Give Mult")]
public class GiveMultEffect : JokerEffect
{

    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(ammount);
        jokerInstance.triggerMessage = "+" + ammount;
        jokerInstance.jokerContainer.TriggerMessage();
    }
}
