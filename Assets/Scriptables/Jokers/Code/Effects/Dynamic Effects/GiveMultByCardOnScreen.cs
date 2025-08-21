using UnityEngine;
[CreateAssetMenu(fileName = "Give Mult by Required Card", menuName = "Scriptables/Joker/Effect/Give Mult per Required Card")]
public class GiveMultByCardOnScreen : JokerEffect
{
    public int multammount;
    public int requiredNumber;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        if (CardPlayer.instance.currentHand.Count > 0)
        {
            ScoreManager.instance.AddMult(CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumber).Count * multammount);
        }
    }

    public override string GetCustomMessage()
    {
        return "+"+(CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumber).Count * multammount);
    }
}
