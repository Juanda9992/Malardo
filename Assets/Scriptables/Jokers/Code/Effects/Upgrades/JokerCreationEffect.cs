using UnityEngine;
[CreateAssetMenu(fileName = "Create Joker", menuName = "Scriptables/Joker/Effect/Upgrade/Create Joker")]
public class JokerCreationEffect : JokerEffect
{
    public int jokerAmmount;
    public JokerRarity jokerRarity;
    public override void ApplyEffect(JokerInstance instance)
    {
        for (int i = 0; i < jokerAmmount; i++)
        {
            if (JokerManager.instance.CanAddJoker())
            {
                JokerManager.instance.AddJoker(DatabaseManager.instance.jokerContainer.GetRandomJokerByRarity(jokerRarity).data);
                instance.triggerMessage = "Create!";
                instance.jokerContainer.TriggerMessage();
            }
        }
    }
}
