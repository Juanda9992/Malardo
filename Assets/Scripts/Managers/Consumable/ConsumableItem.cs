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
    [SerializeField] private Button upperButton;

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    [SerializeField] private TextMeshProUGUI buyButtonText;
    [SerializeField] private int buyValue = 3;
    [SerializeField] private int sellValue;

    [SerializeField] private UnityEvent SetUpShopControls, SetUpConsumableControls;

    private bool directlyused;
    public void SetPlanetData(PlanetCardData planetCard)
    {
        planetCardData = planetCard;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(planetCardData.handType);

        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        textDescription.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc, DescriptionType.Planet);

        SetPrice();
        SetUpButtons();
    }
    public void UseItem()
    {
        if (!isOnShop)
        {
            if (!directlyused)
            {
                ConsumableManager.instance.DecreaseConsumable();
            }
            PokerHandUpgrader.instance.RequestUpgradeHand(planetCardData.handType);
            StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnPlanetCardUse));
            Destroy(gameObject);
        }
        else
        {
            if (!ConsumableManager.instance.CanAddConsumable)
            {
                FullSlotAnimation.instance.ShowConsumableAnimation();
                return;
            }
            CurrencyManager.instance.RemoveCurrency(buyValue);
            ConsumableManager.instance.AddFromShop(this);
        }
    }

    private void SetPrice()
    {
        sellButtonText.text = $"Sell \n ${sellValue}";

        if (isOnShop)
        {
            if (planetCardData != null)
            {
                if (DatabaseManager.instance.pricesDatabase.freePlanetPacks)
                {
                    buyValue = 0;
                }
            }
            buyButtonText.text = "Buy $" + buyValue;
        }
    }

    public void SellItem()
    {
        if (!isOnShop)
        {
            ConsumableManager.instance.DecreaseConsumable();
            CurrencyManager.instance.AddCurrency(sellValue);
            Destroy(gameObject);
        }
        else
        {
            isOnShop = false;
            directlyused = true;
            CurrencyManager.instance.RemoveCurrency(buyValue);
            UseItem();
        }
    }

    public void SetUpButtons()
    {
        if (isOnShop)
        {
            SetUpShopControls?.Invoke();

            bottomButton.interactable = CurrencyManager.instance.currentCurrency >= buyValue;
            upperButton.interactable = bottomButton.interactable;

            upperButton.targetGraphic.color = Color.magenta;
            sellButtonText.text = "BUY & USE";

        }
        else
        {
            SetUpConsumableControls?.Invoke();
            upperButton.targetGraphic.color = Color.green;
            sellButtonText.text = "SELL $"+sellValue;
        }
    }
}

