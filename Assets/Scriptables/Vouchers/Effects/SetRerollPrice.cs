using UnityEngine;
[CreateAssetMenu(fileName = "Set Reroll Price",menuName = "Scriptables/Voucher/Effect/Set Reroll Price")]
public class SetRerollPrice : CardEffect
{
    public int rerollPrice;

    public override void ApplyEffect()
    {
        RerollShop.instance.SetRerollPrice(rerollPrice);
    }
}
