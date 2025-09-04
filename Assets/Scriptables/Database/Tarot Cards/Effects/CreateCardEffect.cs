using UnityEngine;

[CreateAssetMenu(fileName = "Create Card",menuName ="Scriptables/Tarot Card/Effect/Create Card")]
public class CreateCardEffect : CardEffect
{
    public bool planetCards;
    public bool tarotCards;
    public int maxCard;

    public override void ApplyEffect()
    {
        for (int i = 0; i < maxCard; i++)
        {
            if (ConsumableManager.instance.CanAddConsumable)
            {
                if (tarotCards)
                {
                    TarotCardData tarotCardData = DatabaseManager.instance.tarotCardDatabase.GetRandomTarotCard();
                    ConsumableManager.instance.GenerateTarotCard(tarotCardData);
                }
                if (planetCards)
                {
                    PlanetCardData planetCardData = DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard();
                    ConsumableManager.instance.GeneratePlanetCard(planetCardData);
                }

            }
        }
    }
}
