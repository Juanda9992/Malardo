using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips per Remaining Card", menuName = "Scriptables/Joker/Effect/Give Chips per Remaining Card")]
public class GiveChipsPerRemainingCard : JokerEffect
{
    public int chipAmmount;
    public override void ApplyEffect()
    {
        ScoreManager.instance.AddChips(chipAmmount * CardManager.instance.cards.Count);
    }

    public override string GetCustomMessage()
    {
        return (chipAmmount * CardManager.instance.cards.Count).ToString();
    }
}
