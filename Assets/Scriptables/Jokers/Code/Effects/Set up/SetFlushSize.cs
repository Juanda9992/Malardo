using UnityEngine;
[CreateAssetMenu(fileName ="Set Flush SIze",menuName = "Scriptables/Joker/Effect/Set up/Set Flush Size")]
public class SetFlushSize : JokerEffect
{
    public int newSize;

    public override void ApplyEffect()
    {
        HandDetector.instance.requiredAmmountForFlush = newSize;
    }
}
