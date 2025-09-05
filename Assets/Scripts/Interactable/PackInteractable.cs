using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackInteractable : MonoBehaviour
{
    public PackType itemType;
    public JokerData jokerData;
    public PackData packData;
    public TarotCardData tarotCard;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Button actionButton;
    private Card createdCard;
    private PlanetCardData _planetCardData;
    public void SetJokerInfo(JokerInstance createdJoker)
    {
        jokerData = createdJoker.data;
        itemType = PackType.Buffon;
        GetComponent<DescriptionContainer>().SetNameAndDescription(jokerData.jokerName, jokerData.description, GetTypeByjokerRarity(createdJoker));
        cardName.text = createdJoker.data.jokerName;
        StartCoroutine("ListenForAvaliability");
    }

    public DescriptionType GetTypeByjokerRarity(JokerInstance jokerInstance)
    {
        if (jokerInstance.data.jokerRarity == JokerRarity.Common)
        {
            return DescriptionType.Common;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Uncommon)
        {
            return DescriptionType.Uncommon;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Rare)
        {
            return DescriptionType.Rare;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Legendary)
        {
            return DescriptionType.Legendary;
        }

        return DescriptionType.None;
    }

    public void SetPackCard()
    {
        itemType = PackType.Card;
        createdCard = new Card();
        createdCard = createdCard.GenerateRandomCard();

        GetComponent<CardVisuals>().SetVisuals(createdCard);
        GetComponent<DescriptionContainer>().SetNameAndDescription(createdCard.cardName, "+" + createdCard.chipAmmount + " chips", DescriptionType.None);
    }

    public void SetPlanetCard(PlanetCardData planetCardData)
    {
        _planetCardData = planetCardData;
        itemType = PackType.Planet;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(_planetCardData.handType);
        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        cardName.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc, DescriptionType.Planet);
    }

    public void SetTarotData(TarotCardData tarotCardData)
    {
        itemType = PackType.Tarot;
        tarotCard = tarotCardData;
        cardName.text = tarotCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(tarotCardData.cardName, tarotCardData.cardDescription, DescriptionType.Tarot);
        StartCoroutine("ListenForAvaliability");
    }

    public IEnumerator ListenForAvaliability()
    {
        while (true)
        {
            if (itemType == PackType.Buffon)
            {
                actionButton.interactable = JokerManager.instance.CanAddJoker();
            }

            if (itemType == PackType.Tarot)
            {
                actionButton.interactable = tarotCard.CanApplyEffect();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void SelectItem()
    {
        switch (itemType)
        {
            case PackType.Buffon:
                JokerManager.instance.AddJoker(jokerData);
                break;
            case PackType.Card:
                DeckManager.instance.AddCardOnFullDeck(createdCard);
                break;
            case PackType.Planet:
                PokerHandUpgrader.instance.RequestUpgradeHand(_planetCardData.handType);
                StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnPlanetCardUse));
                CardManager.instance.UpdateLastCard(_planetCardData);
                break;
            case PackType.Tarot:
                tarotCard.cardEffect.ApplyEffect();

                if (tarotCard.saveCard)
                {
                    tarotCard.SaveCard();
                }
                break;
        }
        PackManager.instance.SelectItem();
    }
}
