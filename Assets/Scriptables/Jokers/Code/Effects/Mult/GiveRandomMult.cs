using UnityEngine;

[CreateAssetMenu(fileName = "Give Random Mult", menuName = "Scriptables/Joker/Effect/Give Random Mult")]
public class GiveRandomMult : JokerEffect
{
    public int minRange, maxRange;
    private int multGiven;
    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(multGiven);
    }

    public override string GetCustomMessage()
    {
        multGiven = Random.Range(minRange, maxRange);
        return "+" +multGiven.ToString();
    }
}
