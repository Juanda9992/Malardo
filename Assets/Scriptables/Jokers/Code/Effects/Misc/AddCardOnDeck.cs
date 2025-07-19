using UnityEngine;
[CreateAssetMenu(fileName = "Add Card on Deck", menuName = "Scriptables/Joker/Effect/Misc/Add card on full deck")]
public class AddCardOnDeck : JokerEffect
{
    public Card card;
    public override void ApplyEffect()
    {
        DeckManager.instance.AddCardOnFullDeck(card);
    }
}
