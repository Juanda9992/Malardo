using UnityEngine;

[CreateAssetMenu(fileName = "Multi Multiplier", menuName = "Scriptables/Joker/Effect/Multiply Multi")]
public class MultMultiplierEffect : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.MultiplyMulti(ammount);
    }
}
