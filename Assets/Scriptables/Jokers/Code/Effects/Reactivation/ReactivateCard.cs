using UnityEngine;
[CreateAssetMenu(fileName = "Reactivate Card",menuName = "Scriptables/Joker/Effect/Reactivation/Reactivate Card")]
public class ReactivateCard : JokerEffect
{
    public int reactivationTimes;
    public bool firstCard;
    public bool faceCards;

    public override void ApplyEffect()
    {

    }
    public override int CheckForActivation(Card cardToPlay)
    {
        int activations = 0;
        if (firstCard)
        {
            if (cardToPlay == CardPlayer.instance.currentHand[0])
            {
                activations += reactivationTimes;
            }
        }

        if (faceCards)
        {
            if (cardToPlay.IsFaceCard)
            {
                activations += reactivationTimes;
            }
        }

        return cardToPlay.activations + activations;
    }
}
