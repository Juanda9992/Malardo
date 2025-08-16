using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Debuff Numbers", menuName = "Scriptables/Blind/Debuff Number")]
public class DebuffCustomCards : CurrentBlind
{
    public bool faceCards;

    public static int[] numbersDebuffed;
    public static Action<int[]> OnNumbersDebuffed;

    public static bool isActive;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        isActive = true;
        if (faceCards)
        {
            numbersDebuffed = new int[3] { 11, 12, 13 };

            if (Card.overrideFaceCard)
            {
                numbersDebuffed = new int[13] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            }
            OnNumbersDebuffed?.Invoke(numbersDebuffed);
        }
    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        isActive = false;
        Card.overrideFaceCard = false;
        numbersDebuffed = null;
        OnNumbersDebuffed?.Invoke(null);
    }
}
