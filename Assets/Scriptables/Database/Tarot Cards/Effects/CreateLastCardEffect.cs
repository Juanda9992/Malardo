using UnityEngine;

[CreateAssetMenu(fileName = "Generate last Card",menuName = "Scriptables/Tarot Card/Effect/Generate Last Card")]
public class CreateLastCardEffect : CardEffect
{
    bool planetCard;
    bool tarotCard;
    bool canAddConsumable;
    public override void ApplyEffect()
    {
        if (planetCard)
        {
            ConsumableManager.instance.GeneratePlanetCard(CardManager.instance.lastPlanetCard);
        }
        if (tarotCard)
        {
            ConsumableManager.instance.GenerateTarotCard(CardManager.instance.lastTarotCard);
        }
    }

    public override bool CanBeUsed()
    {
        planetCard = CardManager.instance.lastPlanetCard != null;
        tarotCard = CardManager.instance.lastTarotCard != null;
        canAddConsumable = ConsumableManager.instance.CanAddConsumable;
        return (planetCard || tarotCard) && canAddConsumable;
    }
}
