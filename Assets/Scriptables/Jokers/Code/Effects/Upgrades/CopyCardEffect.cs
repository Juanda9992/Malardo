using UnityEngine;
[CreateAssetMenu(fileName = "Copy Card",menuName = "Scriptables/Joker/Effect/Misc/Copy Card")]
public class CopyCardEffect : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        Card newCard = new Card(GameStatusManager._Status.cardPlayed);
        DeckManager.instance.AddCardOnFullDeck(newCard);
        CardManager.instance.GenerateCardOnHand(newCard);
    }
}
