using System.Collections;
using Shop.Voucher;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoucherInteractable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI voucherText, buyButtonText;
    [SerializeField] private VoucherData voucherData;
    [SerializeField] private DescriptionContainer descriptionContainer;
    [SerializeField] private Button buyButton;

    void Start()
    {
        StartCoroutine(nameof(UpdateButtonStatus));
    }
    public void SetVoucherData(VoucherData data)
    {
        voucherData = data;
        voucherText.text = data.voucherName;
        buyButtonText.text = "Buy $10";
        buyButton.interactable = CurrencyManager.instance.OverMinDebt(10);
        descriptionContainer.SetNameAndDescription(data.voucherName, data.voucherDescription, DescriptionType.Voucher);
    }
    public void ConsumeItem()
    {
        if (voucherData.voucherEffect != null)
        {
            voucherData.voucherEffect.ApplyEffect();
        }
        JokerDescription.instance.SetDescriptionOff();
        CurrencyManager.instance.RemoveCurrency(10);
        DatabaseManager.instance.matchVoucherDatabase.SetVoucherBought(voucherData);
        ShopManager.instance.ShowVoucherLabel();
        Destroy(gameObject);
    }
    [ContextMenu("Set Custom Data")]
    private void SetCustomData()
    {
        SetVoucherData(voucherData);
    }

    private IEnumerator UpdateButtonStatus()
    {
        while (true)
        {
            buyButton.interactable = CurrencyManager.instance.OverMinDebt(10);
            buyButtonText.text = "Buy $" + 10;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
