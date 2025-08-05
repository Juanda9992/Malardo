using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Debuff Numbers", menuName = "Scriptables/Blind/Debuff Number")]
public class DebuffCustomCards : CurrentBlind
{
    public bool faceCards;

    public static int[] numbersDebuffed;
    public static Action<int[]> OnNumbersDebuffed;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        if (faceCards)
        {
            numbersDebuffed = new int[3] { 11, 12, 13 };
            OnNumbersDebuffed?.Invoke(numbersDebuffed);
        }
    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        numbersDebuffed = null;
        OnNumbersDebuffed?.Invoke(null);
    }
}
