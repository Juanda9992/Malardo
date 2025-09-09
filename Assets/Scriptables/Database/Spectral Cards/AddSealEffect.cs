using UnityEngine;
[CreateAssetMenu(fileName = "Add Seal",menuName = "Scriptables/Tarot Card/Effect/Add Seal")]
public class AddSealEffect : CardEffect
{
    public Seal seal;
    public override void ApplyEffect()
    {
        HandManager.instance.handCards[0].cardSeal = seal;
        HandManager.instance.handCards[0].linkedCard.visuals.SetVisuals(HandManager.instance.handCards[0]);
    }

    public override bool CanBeUsed()
    {
        return HandManager.instance.handCards.Count == 1;
    }
}
