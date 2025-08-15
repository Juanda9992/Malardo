using UnityEngine;
[CreateAssetMenu(fileName = "Add mult by hand played x times",menuName = "Scriptables/Joker/Effect/Mult/Add mult by hand played x times")]
public class AddMultByTimesHandPlayed : JokerEffect
{
    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(GetValue());
    }

    private int GetValue()
    {
        return PokerHandLevelStorage.instance.GetHandData(HandDetector.instance.currentHand.pokerHand.handType).handPlayedTime;
    }

    public override string GetCustomMessage()
    {
        return "+" + GetValue();
    }
}
