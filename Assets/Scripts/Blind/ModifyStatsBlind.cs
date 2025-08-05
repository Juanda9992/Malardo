
using UnityEngine;
[CreateAssetMenu(fileName = "Modify Stats Blind",menuName = "Scriptables/Blind/Modify Stats Blind")]
public class ModifyStatsBlind : CurrentBlind
{
    public bool noDiscards;


    private int lastDiscards;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        if (noDiscards)
        {
            lastDiscards = HandManager.instance.defaultDiscards;
            HandManager.instance.SetDefaultDiscards(0);
        }
    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        if (noDiscards)
        {
            HandManager.instance.SetDefaultDiscards(lastDiscards,true);
        }
    }
}
