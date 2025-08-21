using UnityEngine;

[CreateAssetMenu(fileName = "JokerEffect", menuName = "Scriptables/Joker/Give Chips")]
public class GiveChipEffect : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {

        ScoreManager.instance.AddChips((int)ammount);
    }
}