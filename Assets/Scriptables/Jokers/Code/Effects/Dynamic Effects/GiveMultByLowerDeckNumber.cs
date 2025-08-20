using UnityEngine;

[CreateAssetMenu(fileName = "Get Mult by lower deck count",menuName = "Scriptables/Joker/Effect/Mult/Give Mult by lower deck")]
public class GiveMultByLowerDeckNumber : JokerEffect
{
    public int multammount;
    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(GetCalculation());
    }

    public override string GetCustomMessage()
    {
        return "+" + GetCalculation();
    }

    private int GetCalculation()
    {
        int result = DeckManager.instance.initialDeckSize - DeckManager.instance.roundDeckSize;

        result = result < 0 ? 0 : result;

        Debug.Log(DeckManager.instance.initialDeckSize + "" + DeckManager.instance.roundDeckSize);
        return result * multammount;
    }
}
