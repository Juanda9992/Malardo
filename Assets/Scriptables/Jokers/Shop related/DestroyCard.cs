using UnityEngine;
[CreateAssetMenu(fileName = "Destroy Card",menuName ="Scriptables/Joker/Effect/Misc/Destroy Card")]
public class DestroyCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        HandManager.instance.handCards[0].linkedCard.pointerInteraction.DestroyCard();
    }
}
