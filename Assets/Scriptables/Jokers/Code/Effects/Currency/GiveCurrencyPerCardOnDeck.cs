using UnityEngine;

[CreateAssetMenu(fileName = "Give Currency per Card on Full Deck", menuName = "Scriptables/Joker/Effect/Give Currency per Card on Full Deck")]
public class GiveCurrencyPerCardOnDeck : JokerEffect
{
    public int requiredNumber;
    public override void ApplyEffect()
    {
        CurrencyManager.instance.AddCurrency(DeckManager.instance.GetAllCardsOnFullDeckByNumber(9));
    }

    public override string GetCustomMessage()
    {
        return "$" + DeckManager.instance.GetAllCardsOnFullDeckByNumber(9);
    }
}
