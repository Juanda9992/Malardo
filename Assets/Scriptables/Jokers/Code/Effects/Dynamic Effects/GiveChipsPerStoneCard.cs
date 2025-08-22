using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips per StoneCard",menuName = "Scriptables/Joker/Effect/Give Chips per Stone Card")]
public class GiveChipsPerStoneCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips(CalculateChips());
        jokerInstance.triggerMessage = "+" + CalculateChips();
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", CalculateChips().ToString());
        jokerInstance.jokerContainer.TriggerMessage();
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", CalculateChips().ToString());
    }

    private int CalculateChips()
    {
        return (int)ammount * DeckManager.instance.GetAllStoneCardsOnFullDeck();
    }
}
