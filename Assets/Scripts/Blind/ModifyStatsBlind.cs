
using UnityEngine;
[CreateAssetMenu(fileName = "Modify Stats Blind", menuName = "Scriptables/Blind/Modify Stats Blind")]
public class ModifyStatsBlind : CurrentBlind
{
    public bool noDiscards;
    public int handAmmount = -1;


    private int lastDiscards;
    private int lastHands;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        if (noDiscards)
        {
            lastDiscards = HandManager.instance.defaultDiscards;
            HandManager.instance.SetDefaultDiscards(0);
        }
        if (handAmmount > 0)
        {
            lastHands = HandManager.instance.GetHandsRemaining();
            HandManager.instance.SetDefaultHands(handAmmount, true);
        }

    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        if (noDiscards)
        {
            HandManager.instance.SetDefaultDiscards(lastDiscards, true);
        }

        if (handAmmount > 0)
        {
            HandManager.instance.SetDefaultHands(lastHands, true);
        }
    }
}
