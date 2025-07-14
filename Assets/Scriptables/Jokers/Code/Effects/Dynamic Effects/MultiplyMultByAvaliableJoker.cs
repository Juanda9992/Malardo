using UnityEngine;
[CreateAssetMenu(fileName = "Multiply multi per avaliable Joker", menuName = "Scriptables/Joker/Effect/Multiply Mult per Avaliable Joker")]
public class MultiplyMultByAvaliableJoker : JokerEffect
{
    public override void ApplyEffect()
    {
        ScoreManager.instance.MultiplyMulti(JokerManager.instance.maximumJokers - JokerManager.instance.currentJokers.Count);
    }

    public override string GetCustomMessage()
    {
        return "X" + (JokerManager.instance.maximumJokers - JokerManager.instance.currentJokers.Count).ToString();
    }
}
