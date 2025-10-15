using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Random Poker Hand", menuName = "Scriptables/Tag/Effect/Upgrade Poker Hand")]
public class UpgradeRandomPokerHand : CardEffect
{
    public override void ApplyEffect(HandType handType)
    {
        Debug.Log(handType);
        PokerHandUpgrader.instance.RequestUpgradeHand(handType, 3);
    }

    public override string GetDescription(string baseDescriptio,HandType hand)
    {
        Debug.Log(hand);
        return baseDescriptio.Replace("_R_",CommonOperations.ParseHandType(hand));
    }
}
