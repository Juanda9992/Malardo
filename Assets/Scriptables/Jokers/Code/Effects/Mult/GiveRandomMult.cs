using UnityEngine;

[CreateAssetMenu(fileName = "Give Random Mult", menuName = "Scriptables/Joker/Effect/Give Random Mult")]
public class GiveRandomMult : JokerEffect
{
    public int minRange, maxRange;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        int mult = Random.Range(minRange, maxRange);
        ScoreManager.instance.AddMult(mult);
        jokerInstance.triggerMessage = "+" + mult.ToString();
        jokerInstance.jokerContainer.TriggerMessage();
    }

}
