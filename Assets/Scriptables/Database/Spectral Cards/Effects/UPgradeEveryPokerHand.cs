using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade All poker Hands",menuName = "Scriptables/Tarot Card/Effect/Upgrade All Poker Hands")]
public class UPgradeEveryPokerHand : CardEffect
{
    public override void ApplyEffect()
    {
        PokerHandUpgrader.instance.RequestUpgradeAllHands();
    }
}
