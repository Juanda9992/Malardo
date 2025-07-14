using UnityEngine;

[CreateAssetMenu(fileName = "JokerEffect", menuName = "Scriptables/Joker/Give Chips")]
public class GiveChipEffect : JokerEffect
{
    public override void ApplyEffect()
    {

        ScoreManager.instance.AddChips((int)ammount);
    }
}