using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Blind", menuName = "Scriptables/Blind/Debuff Suit")]
public class DebuffSuitBlind : CurrentBlind
{
    public static Action<Suit, bool> OnDebuffBlindStatus;
    public static Suit suitDebuffed = Suit.None;
    public Suit suitToDebuff = Suit.None;

    void Awake()
    {
        suitDebuffed = Suit.None;
    }
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        OnDebuffBlindStatus?.Invoke(suitToDebuff, true);
        suitDebuffed = suitToDebuff;
        BlindManager.instance.SetCustomRequiredScore((int)(BlindManager.instance.GetRoundBaseScore() * blindMultiplier));
        Debug.Log(suitToDebuff + " Debuffed");
    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        suitDebuffed = Suit.None; ;
        OnDebuffBlindStatus?.Invoke(suitDebuffed, false);
        Debug.Log(suitToDebuff + " Debuffed");
    }
}
