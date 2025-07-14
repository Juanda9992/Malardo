using UnityEngine;

[CreateAssetMenu(fileName = "Multi Multiplier", menuName = "Scriptables/Joker/Effect/Multiply Multi")]
public class MultMultiplierEffect : JokerEffect
{
    public override void ApplyEffect()
    {
        ScoreManager.instance.MultiplyMulti(ammount);
    }
}
