using UnityEngine;

[CreateAssetMenu(fileName = "Set Hand Size", menuName = "Scriptables/Joker/Effect/Set up/Set Hand Size")]
public class SetHandSize : JokerEffect
{
    public int handAmmount;
    public override void ApplyEffect()
    {
        DeckManager.instance.AddHandSize(handAmmount);
    }
}