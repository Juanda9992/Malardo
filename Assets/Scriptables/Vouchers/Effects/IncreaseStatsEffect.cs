
using UnityEngine;
namespace Effects
{
    [CreateAssetMenu(fileName = "Increase Stat",menuName = "Scriptables/Voucher/Effect/Increase Stat")]
    public class IncreaseStatsEffect : CardEffect
    {
        public bool hands;
        public bool discards;
        public bool handSize;
        public bool jokerSlot;

        public override void ApplyEffect()
        {
            if (hands)
            {
                HandManager.instance.IncreaseHands();
            }

            if (discards)
            {
                HandManager.instance.IncreaseDiscards();
            }

            if (handSize)
            {
                DeckManager.instance.AddHandSize(1);
            }

            if (jokerSlot)
            {
                JokerManager.instance.IncreaseMaxJokers();
            }
        }
    }
}
