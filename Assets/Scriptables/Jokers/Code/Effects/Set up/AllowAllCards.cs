using UnityEngine;

[CreateAssetMenu(fileName = "Allow All Cards",menuName = "Scriptables/Joker/Effect/Misc/Allow All Cards")]
public class AllowAllCards : JokerEffect
{
    public bool allowState;
    public override void ApplyEffect(JokerInstance instance)
    {
        HandDetector.instance.allowAllCards = allowState;
    }
}
