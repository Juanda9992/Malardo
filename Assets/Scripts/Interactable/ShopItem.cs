using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int ammountRequired;

    [SerializeField] private TextMeshProUGUI buyLabel;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Button interactButton;
    [SerializeField] private PackType packType = PackType.None;
    private PackData _itemPack;
    void Start()
    {
        UpdateBuyButtonStatus(CurrencyManager.instance.currentCurrency);
    }

    private void UpdateBuyButtonStatus(int ammount)
    {
        interactButton.interactable = ammount > ammountRequired;
        buyLabel.text = "Buy $" + ammountRequired;
    }
    public void BuyItem()
    {
        CurrencyManager.instance.RemoveCurrency(ammountRequired);
        if (packType != PackType.None)
        {
            PackManager.instance.ReceiveCreatePackInstruction(_itemPack);
        }
        JokerDescription.instance.SetDescriptionOff();
    }

    public void SetPackData(PackData data)
    {
        GetComponent<DescriptionContainer>().SetNameAndDescription(data.packName, data.packDescription);

        ammountRequired = data.packCost;
        if (data.packType == PackType.Planet)
        {
            ammountRequired = DatabaseManager.instance.pricesDatabase.freePlanetPacks ? 0 : data.packCost;
        }
        packType = data.packType;
        contentText.text = data.packName;
        _itemPack = data;
    }

    void OnEnable()
    {
        CurrencyManager.OnMoneyChanged += UpdateBuyButtonStatus;
    }

    void OnDisable()
    {
        CurrencyManager.OnMoneyChanged -= UpdateBuyButtonStatus;
    }
}
