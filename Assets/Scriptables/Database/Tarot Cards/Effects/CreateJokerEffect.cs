using UnityEngine;

[CreateAssetMenu(fileName = "Create Random Joker", menuName = "Scriptables/Tarot Card/Effect/Create Joker")]
public class CreateJokerEffect : CardEffect
{
    public bool anyJoker;
    public JokerRarity desiredRarity;
    public bool blankCurrency;
    public override bool CanBeUsed()
    {
        return JokerManager.instance.CanAddJoker();
    }
    public override void ApplyEffect()
    {
        JokerData jokerData;

        if (anyJoker)
        {
            jokerData = DatabaseManager.instance.jokerContainer.GetRandomJoker().data;
        }
        else
        {
            jokerData = DatabaseManager.instance.jokerContainer.GetRandomJokerByRarity(desiredRarity).data;

        }

        if (blankCurrency)
        {
            CurrencyManager.instance.SetCurrency(0);
        }
            JokerInstance instance = new JokerInstance(jokerData);
        JokerManager.instance.AddJoker(instance);
        JokerManager.instance.StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnTarotCardUsed));
    }
}
