using UnityEngine;

namespace Shop.Voucher
{
    [CreateAssetMenu(menuName = "Scriptables/Voucher",fileName = "Voucher Data")]
    public class VoucherData : ScriptableObject
    {
        public string voucherName;
        [TextArea] public string voucherDescription;
        public int basePrice = 10;
        public CardEffect voucherEffect;
    }
}
