using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConsumableItem : MonoBehaviour
{
    public PlanetCardData planetCardData;
    public TarotCardData tarotCardData;
    public bool isOnShop = false;

    [SerializeField] private Button bottomButton;
    [SerializeField] private Button upperButton;
    [SerializeField] private Button interactButton;

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    [SerializeField] private TextMeshProUGUI buyButtonText;
    [SerializeField] private int buyValue = 3;
    [SerializeField] private int sellValue;

    [SerializeField] private UnityEvent SetUpShopControls, SetUpConsumableControls;

    private bool directlyused;
    private DescriptionContainer descriptionContainer;

    void Awake()
    {
        descriptionContainer = GetComponent<DescriptionContainer>();
    }
    public void SetPlanetData(PlanetCardData planetCard)
    {
        planetCardData = planetCard;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(planetCardData.handType);

        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        textDescription.text = planetCardData.cardName;
        descriptionContainer.SetNameAndDescription(planetCardData.cardName, fullDesc, DescriptionType.Planet);

        SetPrice();
        SetUpButtons();
    }

    public void SetTarotData(TarotCardData _tarotCardData)
    {
        tarotCardData = _tarotCardData;

        textDescription.text = tarotCardData.cardName;
        descriptionContainer.SetNameAndDescription(tarotCardData.cardName, _tarotCardData.cardDescription, DescriptionType.Tarot);

        SetPrice();
        SetUpButtons();

        StartCoroutine(ListenForEffect());
    }

    private IEnumerator ListenForEffect()
    {
        while (true)
        {
            if (!isOnShop)
            {
                interactButton.interactable = tarotCardData.CanApplyEffect();
                descriptionContainer.SetNameAndDescription(tarotCardData.cardName, tarotCardData.GetDescription(), DescriptionType.Tarot);

            }
            else
            {
                upperButton.interactable = tarotCardData.CanApplyEffect();
                descriptionContainer.SetNameAndDescription(tarotCardData.cardName, tarotCardData.GetDescription(), DescriptionType.Tarot);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void UseItem()
    {
        if (!isOnShop)
        {
            if (!directlyused)
            {
                ConsumableManager.instance.DecreaseConsumable();
            }
            SwitchCardBehavior();
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

    private void SwitchCardBehavior()
    {
        if (planetCardData != null)
        {
            PokerHandUpgrader.instance.RequestUpgradeHand(planetCardData.handType);
            StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnPlanetCardUse));
            CardManager.instance.UpdateLastCard(planetCardData);
        }

        if (tarotCardData != null)
        {
            tarotCardData.cardEffect.ApplyEffect();

            if (tarotCardData.saveCard)
            {
                tarotCardData.SaveCard();
            }
        }
        Destroy(gameObject);
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
            JokerManager.instance.StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnCardSold));
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
            if (tarotCardData != null)
            {
                upperButton.interactable = tarotCardData.CanApplyEffect();
            }
            else
            {
                upperButton.interactable = bottomButton.interactable;
            }

            upperButton.targetGraphic.color = Color.magenta;
            sellButtonText.text = "BUY & USE";

        }
        else
        {
            SetUpConsumableControls?.Invoke();
            upperButton.interactable = true;
            upperButton.targetGraphic.color = Color.green;
            sellButtonText.text = "SELL $" + sellValue;
        }
    }

    [ContextMenu("Generate Tarot Card")]
    private void GenerateTarotCard()
    {
        SetTarotData(tarotCardData);
        ConsumableManager.instance.AddFromShop(this);
    }
}

