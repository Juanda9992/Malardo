using UnityEngine;

[CreateAssetMenu(fileName = "Set Straight Gap Size",menuName = "Scriptables/Joker/Effect/Set up/Set Straight gap size")]
public class SetGapSize : JokerEffect
{
    public int gapSize;
    public override void ApplyEffect()
    {
        HandDetector.instance.gapForStraights = gapSize;
    }
}
