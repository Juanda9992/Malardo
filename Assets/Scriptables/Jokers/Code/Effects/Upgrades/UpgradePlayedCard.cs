using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Card Played",menuName = "Scriptables/Joker/Upgrades/Upgrade Played Card")]
public class UpgradePlayedCard : JokerEffect
{
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        DeckManager.instance.GetFullDeckCard(GameStatusManager._Status.cardPlayed).chipAmmount += (int)ammount;
    }
}
