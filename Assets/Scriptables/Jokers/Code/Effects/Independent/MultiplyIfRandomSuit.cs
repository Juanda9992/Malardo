using UnityEngine;
[CreateAssetMenu(fileName = "Suit Operation Effect", menuName = "Scriptables/Joker/Effect/Independent/Suit Operation")]
public class MultiplyIfRandomSuit : JokerEffect
{
    public float operationAmmount;
    public bool multiply;
    public bool matchesCard;
    public bool acumulateChips;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (multiply || matchesCard)
        {
            ScoreManager.instance.MultiplyMulti(operationAmmount);
            instance.triggerMessage = "X" + operationAmmount;
        }
        if (acumulateChips)
        {
            instance.totalChips += (int)operationAmmount;
            instance.triggerMessage = "+" + operationAmmount;
        }

        instance.jokerContainer.TriggerMessage();
    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        UpdateDescription(jokerInstance);
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        if (acumulateChips)
        {
            instance.jokerDescription = instance.jokerDescription.Replace("_R_", instance.totalChips.ToString());
        }
    }

    public override bool Scores(JokerInstance instance)
    {
        Card card;
        if (multiply || matchesCard)
        {
            card = GameStatusManager._Status.cardPlayed;
        }
        else
        {
            card = GameStatusManager._Status.discardData.lastDiscard;
        }
        if (matchesCard)
        {
            Debug.Log("Enter here");
            return (card.cardSuit == instance.randomSuit || card.cardType == CardType.Wild) && card.number == instance.dynamicVariable; 
        }
        return card.cardSuit == instance.randomSuit || card.cardType == CardType.Wild;
    }
}
