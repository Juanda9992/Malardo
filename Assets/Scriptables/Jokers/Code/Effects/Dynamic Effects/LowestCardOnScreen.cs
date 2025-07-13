using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Lowest Card", menuName = "Scriptables/Joker/Effect/Lowest Card to Mult")]
public class LowestCardOnScreen : JokerEffect
{
    int lowestCardNumber;
    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(lowestCardNumber);
    }

    public override string GetCustomMessage()
    {
        lowestCardNumber = (CardManager.instance.cardsOnScreen.OrderBy(x => x.currentCard.number).ToList()[0].currentCard.number) *2;
        return "+" + lowestCardNumber.ToString();
    }


}
