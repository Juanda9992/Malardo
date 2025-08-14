using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConsumableItem : MonoBehaviour
{
    public PlanetCardData planetCardData;
    public bool isOnShop = false;

    [SerializeField] private Button bottomButton;

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    [SerializeField] private TextMeshProUGUI buyButtonText;
    [SerializeField] private int buyValue = 3;
    [SerializeField] private int sellValue;

    [SerializeField] private UnityEvent SetUpShopControls, SetUpConsumableControls;
    public void SetPlanetData(PlanetCardData planetCard)
    {
        planetCardData = planetCard;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(planetCardData.handType);

        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        textDescription.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc);

        SetPrice();
        SetUpButtons();
    }
    public void UseItem()
    {
        if (!isOnShop)
        {
            ConsumableManager.instance.DecreaseConsumable();
            PokerHandUpgrader.instance.RequestUpgradeHand(planetCardData.handType);
            Destroy(gameObject);
        }
        else
        {
            CurrencyManager.instance.RemoveCurrency(buyValue);
            ConsumableManager.instance.AddFromShop(this);
        }
    }

    private void SetPrice()
    {
        sellButtonText.text = $"Sell \n ${sellValue}";

        if (isOnShop)
        {
            buyButtonText.text = "Buy $" + buyValue;
        }
    }

    public void SellItem()
    {
        ConsumableManager.instance.DecreaseConsumable();
        CurrencyManager.instance.AddCurrency(sellValue);
        Destroy(gameObject);
    }

    public void SetUpButtons()
    {
        if (isOnShop)
        {
            SetUpShopControls?.Invoke();

            bottomButton.interactable = CurrencyManager.instance.currentCurrency >= buyValue;
        }
        else
        {
            SetUpConsumableControls?.Invoke();
        }
    }
}

