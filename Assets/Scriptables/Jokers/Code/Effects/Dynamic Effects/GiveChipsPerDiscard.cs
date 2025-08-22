using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips By Discard", menuName = "Scriptables/Joker/Effect/Give Chips by Discard")]
public class GiveChipsPerDiscard : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(CalculateChips());
        jokerInstance.triggerMessage = "+" + CalculateChips();
        jokerInstance.jokerContainer.TriggerMessage();
    }
    private int CalculateChips()
    {
        return (int)ammount * GameStatusManager._Status.discardsRemaining;
    }

}
