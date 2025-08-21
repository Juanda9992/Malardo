using UnityEngine;
[CreateAssetMenu(fileName = "Add Free Rerolls",menuName = "Scriptables/Joker/Effect/Set up/Add Free Rerolls")]
public class AddFreeRerolls : JokerEffect
{
    public int rerollAmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        RerollShop.freeRerollsValue += rerollAmount;
    }
}
