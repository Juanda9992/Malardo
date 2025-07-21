using UnityEngine;
[CreateAssetMenu(fileName = "Add mult by hand played x times",menuName = "Scriptables/Joker/Effect/Mult/Add mult by hand played x times")]
public class AddMultByTimesHandPlayed : JokerEffect
{
    public HandType[] requiredHand;
    public bool checkInRound;
    public override void ApplyEffect()
    {
        Debug.Log(GameStatusManager._Status.handPlayedData.CheckNumberHandPlayed(requiredHand, checkInRound));
        ScoreManager.instance.AddMult(GameStatusManager._Status.handPlayedData.CheckNumberHandPlayed(requiredHand,checkInRound));
    }

    public override string GetCustomMessage()
    {
        return "+"+GameStatusManager._Status.handPlayedData.CheckNumberHandPlayed(requiredHand,checkInRound).ToString();
    }
}
