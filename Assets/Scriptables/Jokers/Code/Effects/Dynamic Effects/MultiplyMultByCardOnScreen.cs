using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Multi per Card On Screen", menuName = "Scriptables/Joker/Effect/Multiply Multi by Card on Screen")]
public class MultiplyMultByCardOnScreen : JokerEffect
{
    public int requiredNumber;
    public float multiplier;
    public override void ApplyEffect()
    {
        if (CardPlayer.instance.currentHand.Count > 0)
        {
            ScoreManager.instance.MultiplyMulti(CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumber).Count * multiplier);
        }
    }

    public override string GetCustomMessage()
    {
        return "X" + CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumber).Count * multiplier;
    }
}
