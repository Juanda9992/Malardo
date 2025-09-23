using UnityEngine;
[CreateAssetMenu(fileName = "Suit Operation Effect",menuName = "Scriptables/Joker/Effect/Independent/Suit Operation")]
public class MultiplyÏfRandomSuit : JokerEffect
{
    public float operationAmmount;
    public bool multiply;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (multiply)
        {
            ScoreManager.instance.MultiplyMulti(operationAmmount);
            instance.triggerMessage = "X" + operationAmmount;
        }

        instance.jokerContainer.TriggerMessage();
    }

    public override bool Scores(JokerInstance instance)
    {
        return GameStatusManager._Status.cardPlayed.cardSuit == instance.randomSuit || GameStatusManager._Status.cardPlayed.cardType == CardType.Wild;
    }
}
