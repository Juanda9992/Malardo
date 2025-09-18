using Shop.Voucher;
using TMPro;
using UnityEngine;

public class VoucherInteractable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI voucherText;
    [SerializeField] private VoucherData voucherData;
    [SerializeField] private DescriptionContainer descriptionContainer;
    public void SetVoucherData(VoucherData data)
    {
        voucherData = data;
        voucherText.text = data.voucherName;

        descriptionContainer.SetNameAndDescription(data.voucherName, data.voucherDescription, DescriptionType.Voucher);
    }
    public void ConsumeItem()
    {
        if (voucherData.voucherEffect != null)
        {
            voucherData.voucherEffect.ApplyEffect();
        }
        JokerDescription.instance.SetDescriptionOff();
        DatabaseManager.instance.matchVoucherDatabase.SetVoucherBought(voucherData);
        ShopManager.instance.ShowVoucherLabel();
        Destroy(gameObject);
    }
    [ContextMenu("Set Custom Data")]
    private void SetCustomData()
    {
        SetVoucherData(voucherData);
    }
}
