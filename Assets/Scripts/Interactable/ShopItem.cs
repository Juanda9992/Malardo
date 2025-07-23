using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int ammountRequired;

    [SerializeField] private TextMeshProUGUI buyLabel;
    [SerializeField] private Button interactButton;
    [SerializeField] private PackType packType = PackType.None;
    void Start()
    {
        UpdateBuyButtonStatus();
    }

    private void UpdateBuyButtonStatus()
    {
        interactButton.interactable = CurrencyManager.instance.currentCurrency > ammountRequired;
        buyLabel.text = "Buy $" + ammountRequired;
    }
    public void BuyItem()
    {
        CurrencyManager.instance.RemoveCurrency(ammountRequired);
        if (packType != PackType.None)
        {
            PackManager.instance.ReceiveCreatePackInstruction(packType);
        }
    }
}
