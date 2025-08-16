using UnityEngine;

[CreateAssetMenu(fileName = "Set Override Face Card",menuName = "Scriptables/Joker/Effect/Set up/Override face cards")]
public class OverrideFaceCards : JokerEffect
{
    public bool setFaceCardValue;

    public override void ApplyEffect()
    {
        Card.overrideFaceCard = setFaceCardValue;

        if (DebuffCustomCards.isActive)
        {
            BlindManager.instance.activeBossBlind.ApplyEffect();
        }
    }
}
