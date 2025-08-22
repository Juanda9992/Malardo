using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult per Joker", menuName = "Scriptables/Joker/Effect/Give Mult per Joker")]
public class GiveMultByJoker : JokerEffect
{
    public int multammount;

    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(Calculate());
        jokerInstance.triggerMessage = "+" + Calculate();
        UpdateDescription(jokerInstance);
        jokerInstance.jokerContainer.TriggerMessage();
    }

    private int Calculate()
    {
        return multammount * JokerManager.instance.JokersInHand;
    }
    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", Calculate().ToString());
    }

    public override string GetCustomMessage()
    {
        return (multammount * JokerManager.instance.JokersInHand).ToString();
    }
}
