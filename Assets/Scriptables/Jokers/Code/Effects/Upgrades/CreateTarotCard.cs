using UnityEngine;

[CreateAssetMenu(fileName = "Create Tarot Card",menuName = "Scriptables/Joker/Effect/Upgrade/Create Tarot Card")]
public class CreateTarotCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        if (ConsumableManager.instance.CanAddConsumable)
        {
            ConsumableManager.instance.GenerateTarotCard(DatabaseManager.instance.tarotCardDatabase.GetRandomTarotCard());
        }
    }
}
