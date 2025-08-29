using UnityEngine;

[CreateAssetMenu(fileName = "Reduce Hand Size",menuName = "Scriptables/Joker/Effect/Instance Effects/Reduce Hand Size")]
public class DynamicHandSizeEffect : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        DeckManager.instance.AddHandSize(-1);
    }
    public override void SetupEffect(JokerInstance jokerInstance)
    {
        DeckManager.instance.AddHandSize(jokerInstance.dynamicVariable);
    }
}
