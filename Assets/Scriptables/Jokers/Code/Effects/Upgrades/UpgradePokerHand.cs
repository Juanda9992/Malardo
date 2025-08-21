using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Poker Hand",menuName = "Scriptables/Joker/Effect/Upgrade/Upgrade Poker Hand")]
public class UpgradePokerHand : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        Debug.Log("Upgraded");
        PokerHandUpgrader.instance.RequestUpgradeHand(HandDetector.instance.currentHand.pokerHand.handType);
    }
}
