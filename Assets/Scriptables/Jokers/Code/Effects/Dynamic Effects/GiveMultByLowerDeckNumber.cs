using UnityEngine;

[CreateAssetMenu(fileName = "Get Mult by lower deck count",menuName = "Scriptables/Joker/Effect/Mult/Give Mult by lower deck")]
public class GiveMultByLowerDeckNumber : JokerEffect
{
    public int multammount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(GetCalculation());
        jokerInstance.triggerMessage = $"+{GetCalculation()}";
        jokerInstance.jokerDescription = jokerInstance.data.description.Replace("_R_", GetCalculation().ToString());
    }


    private int GetCalculation()
    {
        int result = DeckManager.instance.initialDeckSize - DeckManager.instance.roundDeckSize;

        result = result < 0 ? 0 : result;
        return result * multammount;
    }
}
