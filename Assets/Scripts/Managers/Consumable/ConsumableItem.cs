using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public PlanetCardData planetCardData;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    [SerializeField] private int sellValue;
    public void SetPlanetData(PlanetCardData planetCard)
    {
        planetCardData = planetCard;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(planetCardData.handType);

        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        textDescription.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc);

        SetPrice();
    }
    public void UseItem()
    {
        ConsumableManager.instance.DecreaseConsumable();
        PokerHandUpgrader.instance.RequestUpgradeHand(planetCardData.handType);
        Destroy(gameObject);
    }

    private void SetPrice()
    {
        sellButtonText.text = $"Sell \n ${sellValue}";
    }

    public void SellItem()
    {
        ConsumableManager.instance.DecreaseConsumable();
        CurrencyManager.instance.AddCurrency(sellValue);
        Destroy(gameObject);
    }
}
