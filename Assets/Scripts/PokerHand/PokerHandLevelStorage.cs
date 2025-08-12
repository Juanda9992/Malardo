
using System.Collections.Generic;
using UnityEngine;

public class PokerHandLevelStorage : MonoBehaviour
{
    public List<PokerHandLevelData> pokerHands;

    [ContextMenu("Debug Hands")]
    public void DebugAllHands()
    {
        foreach (var hand in pokerHands)
        {
            hand.DebugPokerHandData();
        }
    }
}

[System.Serializable]
public class PokerHandLevelData
{
    public HandData pokerHand;
    public int handLevel = 1;
    public PokerHandLevelData()
    {
        this.handLevel = 1;
    }
    public int GetTotalChips()
    {
        return pokerHand.baseChips + (pokerHand.chipsUpgrade * (handLevel -1));
    }

    public int GetTotalMult()
    {
        return pokerHand.baseMult +(pokerHand.multUpgrade * (handLevel -1));
    }

    public void UpgradeHand()
    {
        handLevel++;
    }

    public void DebugPokerHandData()
    {
        Debug.Log($"{pokerHand.handType} lvl.{handLevel}, Chips{GetTotalChips()} mult {GetTotalMult()}");
    }
}
