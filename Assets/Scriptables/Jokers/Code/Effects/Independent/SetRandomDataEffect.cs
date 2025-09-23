using UnityEngine;
[CreateAssetMenu(fileName = "Set Random Data", menuName = "Scriptables/Joker/Effect/Independent/Set Random Data")]
public class SetRandomDataEffect : JokerEffect
{
    public bool suit;
    public bool handType;
    public bool number;
    public override void ApplyEffect(JokerInstance instance)
    {
        Calculate(instance);
    }

    private void Calculate(JokerInstance instance)
    {
        if (suit)
        {
            instance.randomSuit = CommonOperations.GetRandomSuit();
        }
        if (handType)
        {
            instance.randomHand = CommonOperations.GetRandomHandType();
        }

        if (number)
        {
            instance.dynamicVariable = Random.Range(2, 15);
        }

        instance.jokerContainer.TriggerMessage("Updated");

    }

    public override void SetupEffect(JokerInstance jokerInstance)
    {
        Calculate(jokerInstance);
        UpdateDescription(jokerInstance);
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        if (suit)
        {
            instance.jokerDescription = instance.data.description.Replace("_S_", instance.randomSuit.ToString());
        }
        if (handType)
        {
            instance.jokerDescription = instance.data.description.Replace("_S_", CommonOperations.ParseHandType(instance.randomHand));
        }
        if (number)
        {

            instance.jokerDescription = instance.data.description.Replace("_S_", ParseCardName(instance.dynamicVariable));
        }
    }


    private string ParseCardName(int number)
    {
        string value = number.ToString();

        if (number == 11)
        {
            value = "J";
        }
        if (number== 12)
        {
            value = "Q";
        }

        if (number== 13)
        {
            value = "K";
        }
        if (number== 14)
        {
            value = "Ace";
        }
        return value;
    }
}
