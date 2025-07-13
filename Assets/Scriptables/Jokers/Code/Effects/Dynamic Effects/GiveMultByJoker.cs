using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult per Joker", menuName = "Scriptables/Joker/Effect/Give Mult per Joker")]
public class GiveMultByJoker : JokerEffect
{
    public int multammount;

    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(multammount * JokerManager.instance.JokersInHand);
    }

    public override string GetCustomMessage()
    {
        return (multammount * JokerManager.instance.JokersInHand).ToString();
    }
}
