using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Multi per Card On Screen", menuName = "Scriptables/Joker/Effect/Multiply Multi by Card on Screen")]
public class MultiplyMultByCardOnScreen : JokerEffect
{
    public int[] requiredNumbers;
    public float multiplier;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        if (CardPlayer.instance.currentHand.Count > 0)
        {
            int founds = 0;

            for (int i = 0; i < requiredNumbers.Length; i++)
            {
                founds += CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumbers[i]).Count;
            }

            founds = founds == 0 ? 1 : founds;
            ScoreManager.instance.MultiplyMulti(founds * multiplier);
        }
    }


    public override string GetCustomMessage()
    {
        int founds = 0;

        for (int i = 0; i < requiredNumbers.Length; i++)
        {
            founds += CardManager.instance.cardsOnScreen.FindAll(x => x.currentCard.number == requiredNumbers[i]).Count;
        }
        return "X" + founds * multiplier;
    }
}
