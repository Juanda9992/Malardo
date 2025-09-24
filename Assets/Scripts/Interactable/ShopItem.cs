using System.Collections;
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
        StartCoroutine(nameof(UpdateButtonStatus));
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
        GetComponent<DescriptionContainer>().SetNameAndDescription(data.packName, data.packDescription, DescriptionType.Booster);

        ammountRequired = data.packCost;
        if (data.packType == PackType.Planet)
        {
            ammountRequired = DatabaseManager.instance.pricesDatabase.freePlanetPacks ? 0 : data.packCost;
        }
        packType = data.packType;
        contentText.text = data.packName;
        _itemPack = data;
    }

    private IEnumerator UpdateButtonStatus()
    {
        while (true)
        {
            interactButton.interactable = CurrencyManager.instance.OverMinDebt(ammountRequired);
            buyLabel.text = "Buy $" + ammountRequired;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
