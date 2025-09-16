using UnityEngine;

[CreateAssetMenu(fileName = "Decrease Ante",menuName = "Scriptables/Voucher/Effect/Decrease Ante")]
public class ReduceAnteEffect : CardEffect
{
    public bool decreaseHand;
    public bool decreaseDiscard;
    public override void ApplyEffect()
    {
        BlindManager.instance.DecreaseAnteLevel();

        if (decreaseHand)
        {
            HandManager.instance.DecreaseHands();
        }
        if (decreaseDiscard)
        {
            HandManager.instance.DecreaseDiscards();
        }
    }
}
