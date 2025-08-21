using UnityEngine;

[CreateAssetMenu(fileName = "Give Chips per StoneCard",menuName = "Scriptables/Joker/Effect/Give Chips per Stone Card")]
public class GiveChipsPerStoneCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddChips((int)ammount * DeckManager.instance.GetAllStoneCardsOnFullDeck());
    }

    public override string GetCustomMessage()
    {
        return "+"+((int)ammount * DeckManager.instance.GetAllStoneCardsOnFullDeck()).ToString();
    }
}
