using UnityEngine;

[CreateAssetMenu(fileName = "Create Tarot Card",menuName = "Scriptables/Joker/Effect/Upgrade/Create Tarot Card")]
public class CreateTarotCard : JokerEffect
{
    public bool isTarot;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (ConsumableManager.instance.CanAddConsumable)
        {
            if (isTarot)
            {
                ConsumableManager.instance.GenerateTarotCard(DatabaseManager.instance.tarotCardDatabase.GetRandomTarotCard());
            }
            else
            {
                ConsumableManager.instance.GenerateTarotCard(DatabaseManager.instance.tarotCardDatabase.GetRandomSpectralCard());
            }
            instance.triggerMessage = "Create!";
            instance.jokerContainer.TriggerMessage();
        }
    }
}
