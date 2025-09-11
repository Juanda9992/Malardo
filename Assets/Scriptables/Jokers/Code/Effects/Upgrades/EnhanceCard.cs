using UnityEngine;
[CreateAssetMenu(fileName = "Enhance Played Card",menuName ="Scriptables/Joker/Effect/Misc/Enhance Played Card")]
public class EnhanceCard : JokerEffect
{
    public CardType cardType;
    public override void ApplyEffect(JokerInstance instance)
    {
        GameStatusManager._Status.cardPlayed.cardType = cardType;
        GameStatusManager._Status.cardPlayed.linkedCard.visuals.SetVisuals(GameStatusManager._Status.cardPlayed);
    }
}
