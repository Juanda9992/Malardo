
using System.Collections.Generic;
using UnityEngine;

public class PokerHandLevelStorage : MonoBehaviour
{
    public static PokerHandLevelStorage instance;
    [SerializeField] private List<PokerHandLevelData> pokerHands;

    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Debug Hands")]
    public void DebugAllHands()
    {
        foreach (var hand in pokerHands)
        {
            hand.DebugPokerHandData();
        }
    }

    public void ResetHandsPlayedInRound()
    {
        foreach (var hand in pokerHands)
        {
            hand.playedInRound = false;
        }
    }

    public PokerHandLevelData GetHandData(HandType handType)
    {
        return pokerHands.Find(x => x.pokerHand.handType == handType);
    }

    public List<PokerHandLevelData> GetPokerHands()
    {
        return pokerHands;
    }
}

[System.Serializable]
public class PokerHandLevelData
{
    public HandData pokerHand;
    public int handLevel = 1;
    public int handPlayedTime = 0;
    public bool playedInRound = false;
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

    public void IncreasePlayTime()
    {
        handPlayedTime++;
        playedInRound = true;
        Debug.Log("Played");
    }

    public void DebugPokerHandData()
    {
        Debug.Log($"{pokerHand.handType} lvl.{handLevel}, Chips{GetTotalChips()} mult {GetTotalMult()}");
    }
}
