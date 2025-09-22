using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Lowest Card", menuName = "Scriptables/Joker/Effect/Lowest Card to Mult")]
public class LowestCardOnScreen : JokerEffect
{
    Card lowestCard;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        string trigger;
        if (Calculate() > 0)
        {
            ScoreManager.instance.AddMult(Calculate());
            trigger = "+" + Calculate();
        }
        else
        {
            trigger = "Disabled!";
        }
        ScoreSign.instance.SetMessage(Color.red, trigger, lowestCard.linkedCard.transform.position);
        lowestCard.linkedCard.pointerInteraction.ShakeCard();
    }
    public int Calculate()
    {
        lowestCard = CardManager.instance.cardsOnScreen.OrderBy(x => x.currentCard.number).ToList()[0].currentCard;

        if (lowestCard.canPlay)
        {
            return lowestCard.number * 2;
        }
        else
        {
            return -1;
        }
    }

}
