using UnityEngine;
[CreateAssetMenu(fileName = "Set Reroll Price",menuName ="Scriptables/Tag/Effect/Set Reroll Price")]
public class RerollTag : CardEffect
{
    public override void ApplyEffect()
    {
        RerollShop.instance.SetRerollPrice(0);
    }
}
