using UnityEngine;

[CreateAssetMenu(fileName = "Give Currency per Card on Full Deck", menuName = "Scriptables/Joker/Effect/Give Currency per Card on Full Deck")]
public class GiveCurrencyPerCardOnDeck : JokerEffect
{
    public int requiredNumber;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        CurrencyManager.instance.AddCurrency(Output());
        jokerInstance.triggerMessage = "$" + Output().ToString();
        jokerInstance.jokerContainer.TriggerMessage();
    }

    private int Output()
    {
        return DeckManager.instance.GetAllCardsOnFullDeckByNumber(requiredNumber);
    }
    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", Output().ToString());
    }
}
