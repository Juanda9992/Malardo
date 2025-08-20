using UnityEngine;
[CreateAssetMenu(fileName = "Reactivate Card", menuName = "Scriptables/Joker/Effect/Reactivation/Reactivate Card")]
public class ReactivateCard : JokerEffect
{
    public int reactivationTimes;
    public bool firstCard;
    public bool faceCards;
    public bool lastround;
    public int[] numberRange;

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

        for (int i = 0; i < numberRange.Length; i++)
        {
            if (cardToPlay.number == numberRange[i])
            {
                activations += reactivationTimes;
            }
        }

        if (lastround)
        {
            Debug.Log(HandManager.instance.GetHandsRemaining());
            if (HandManager.instance.GetHandsRemaining() == 0)
            {
                activations++;
            }
        }

        return activations;
    }
}
