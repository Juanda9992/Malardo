using UnityEngine;
[CreateAssetMenu(fileName = "Add jokers sell value to mult",menuName = "Scriptables/Joker/Effect/Mult/Add jokers sell value to Mult")]
public class AddJokerSellValueToMult : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        ScoreManager.instance.AddMult(JokerManager.instance.GetSellValueFromAllJokers());
    }

    public override string GetCustomMessage()
    {
        return "+" +JokerManager.instance.GetSellValueFromAllJokers();
    }
}
