using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips per Remaining Card", menuName = "Scriptables/Joker/Effect/Give Chips per Remaining Card")]
public class GiveChipsPerRemainingCard : JokerEffect
{
    public int chipAmmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(ChipAmmount());
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", ChipAmmount().ToString());
        jokerInstance.triggerMessage = "+" + ChipAmmount();
        jokerInstance.jokerContainer.TriggerMessage();
    }

    private int ChipAmmount()
    {
        return chipAmmount * DeckManager.instance.roundDeck.Count;
    }
}
