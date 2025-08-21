using UnityEngine;

[CreateAssetMenu(fileName = "Destroy Discard Card", menuName = "Scriptables/Joker/Effect/Misc/Destroy Discarded Card")]
public class DestroyDiscardedCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        DeckManager.instance.fullMatchDeck.Remove(GameStatusManager._Status.discardData.discardCards[0]);
    }

    public override string GetCustomMessage()
    {
        return "Destroyed";
    }
}
