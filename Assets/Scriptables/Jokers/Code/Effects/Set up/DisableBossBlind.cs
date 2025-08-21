using UnityEngine;

[CreateAssetMenu(fileName = "Disable Blind Boss",menuName = "Scriptables/Joker/Effect/Set up/Disable Boss")]
public class DisableBossBlind : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        BlindManager.instance.ResetBossBlind();
    }
}
