using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips per Remaining Card", menuName = "Scriptables/Joker/Effect/Give Chips per Remaining Card")]
public class GiveChipsPerRemainingCard : JokerEffect
{
    public int chipAmmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(chipAmmount * DeckManager.instance.roundDeck.Count);
    }

    public override string GetCustomMessage()
    {
        return (chipAmmount * DeckManager.instance.roundDeck.Count).ToString();
    }
}
