using UnityEngine;

[CreateAssetMenu(fileName = "Give Mult Effect", menuName = "Scriptables/Joker/Give Mult")]
public class GiveMultEffect : JokerEffect
{

    public override void ApplyEffect()
    {
        ScoreManager.instance.AddMult(ammount);
    }
}
