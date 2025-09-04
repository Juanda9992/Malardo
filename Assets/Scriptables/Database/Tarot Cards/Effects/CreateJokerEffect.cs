using UnityEngine;

[CreateAssetMenu(fileName = "Create Random Joker", menuName = "Scriptables/Tarot Card/Effect/Create Joker")]
public class CreateJokerEffect : CardEffect
{
    public bool anyJoker;
    public JokerRarity desiredRarity;

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
        JokerManager.instance.AddJoker(jokerData);
    }
}
