using UnityEngine;

[CreateAssetMenu(fileName = "Blind", menuName = "Scriptables/Blind/Custom Size Blind")]
public class CustomBlindSize : CurrentBlind
{
    [SerializeField] private int applyMultiplier;
    [SerializeField] private int revertMultiplier;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        BlindManager.instance.SetCustomRequiredScore(BlindManager.instance.GetRoundBaseScore() * applyMultiplier);
    }

    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        BlindManager.instance.SetCustomRequiredScore(BlindManager.instance.GetRoundBaseScore() * revertMultiplier);
    }
}
