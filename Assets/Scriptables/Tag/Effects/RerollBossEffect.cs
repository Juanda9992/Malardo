using UnityEngine;
[CreateAssetMenu(fileName = "Reroll boss blind",menuName = "Scriptables/Tag/Effect/Reroll Boss Blind")]
public class RerollBossEffect : CardEffect
{
    public override void ApplyEffect()
    {
        BlindReroll.blindReroll.RerollBlind();
    }
}
