using UnityEngine;
[CreateAssetMenu(fileName = "Enable Blind Reroll",menuName = "Scriptables/Voucher/Effect/Enable Blind Reroll")]
public class BlindRerollEffect : CardEffect
{
    public bool enableReroll;
    public bool enableInfiniteReroll;
    public override void ApplyEffect()
    {
        if (enableReroll)
        {
            BlindReroll.blindReroll.EnableBlindReroll();
        }
        if (enableInfiniteReroll)
        {
            BlindReroll.blindReroll.EnableInfiniteReroll();
        }
    }
}
