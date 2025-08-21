using UnityEngine;

[CreateAssetMenu(fileName = "Self Destruct",menuName = "Scriptables/Joker/Handicap/Self Destruct")]
public class SelfDestructEffect : JokerEffect
{
    public int maxRange;
    public int requiredNumber;

    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        if (Random.Range(0, maxRange) == requiredNumber)
        {
            jokerOutput = "Destroy";
        }
    }
}
