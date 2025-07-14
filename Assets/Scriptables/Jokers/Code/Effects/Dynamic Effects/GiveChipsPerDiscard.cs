using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips By Discard", menuName = "Scriptables/Joker/Effect/Give Chips by Discard")]
public class GiveChipsPerDiscard : JokerEffect
{
    public int chipsPerDiscard;
    public override void ApplyEffect()
    {
        chipsPerDiscard = (int)ammount * GameStatusManager._Status.discardsRemaining;
        ScoreManager.instance.AddChips(chipsPerDiscard);
        Debug.Log("Chips given: " + chipsPerDiscard);
    }

    public override string GetCustomMessage()
    {
        return "+" + chipsPerDiscard;
    }
}
