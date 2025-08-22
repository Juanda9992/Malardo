using UnityEngine;
[CreateAssetMenu(fileName = "Multiply multi per avaliable Joker", menuName = "Scriptables/Joker/Effect/Multiply Mult per Avaliable Joker")]
public class MultiplyMultByAvaliableJoker : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.MultiplyMulti(JokerOutput());
        jokerInstance.triggerMessage = "X" + JokerOutput().ToString();
        UpdateDescription(jokerInstance);
        jokerInstance.jokerContainer.TriggerMessage();   
    }

    private int JokerOutput()
    {
        int outPut = JokerManager.instance.maximumJokers - JokerManager.instance.currentJokers.Count;
        outPut = outPut == 0 ? 1 : outPut;

        return outPut;
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", "X"+JokerOutput().ToString());
    }
}
