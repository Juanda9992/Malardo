using UnityEngine;
[CreateAssetMenu(fileName = "Create Random Jokers",menuName = "Scriptables/Tag/Effect/Create Random Jokers")]
public class CreateRandomJokers :CardEffect
{
    public int jokerAmmount;
    public override void ApplyEffect()
    {
        for (int i = 0; i < jokerAmmount; i++)
        {
            if (JokerManager.instance.CanAddJoker())
            {
                JokerData data = DatabaseManager.instance.jokerContainer.GetRandomJokerByRarity(JokerRarity.Common).data;

                JokerInstance instance = new JokerInstance(data);

                JokerManager.instance.AddJoker(instance);
            }
        }
    }
}
