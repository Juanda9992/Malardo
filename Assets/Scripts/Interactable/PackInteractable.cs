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
    [SerializeField] private GameObject[] jokerVisuals;
    private Card createdCard;
    private PlanetCardData _planetCardData;
    private DescriptionContainer description;
    void Awake()
    {
        description = GetComponent<DescriptionContainer>();
    }
    public void SetJokerInfo(JokerInstance createdJoker)
    {
        jokerData = createdJoker.data;
        itemType = PackType.Buffon;

        if (createdJoker.jokerEdition != CardEdition.Base)
        {
            Debug.Log(createdJoker.jokerEdition);
            jokerVisuals[(int)createdJoker.jokerEdition].SetActive(true);
        }
        description.SetNameAndDescription(jokerData.jokerName, createdJoker.jokerDescription, GetTypeByjokerRarity(createdJoker),createdJoker.jokerEdition);
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
        description.SetNameAndDescription(createdCard.cardName, "+" + createdCard.chipAmmount + " chips", DescriptionType.None);
        StartCoroutine("ListenForAvaliability");

    }

    public void SetPlanetCard(PlanetCardData planetCardData)
    {
        _planetCardData = planetCardData;
        itemType = PackType.Planet;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(_planetCardData.handType);
        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        cardName.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc, DescriptionType.Planet);
        StartCoroutine("ListenForAvaliability");

    }

    public void SetTarotData(TarotCardData tarotCardData)
    {
        itemType = PackType.Tarot;
        tarotCard = tarotCardData;
        cardName.text = tarotCardData.cardName;
        description.SetNameAndDescription(tarotCardData.cardName, tarotCardData.cardDescription, DescriptionType.Tarot);
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
                description.SetNameAndDescription(tarotCard.cardName,tarotCard.GetDescription(), DescriptionType.Tarot);
            }
            actionButton.interactable = PackManager.instance.maxSelections > 0;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void SelectItem()
    {
        switch (itemType)
        {
            case PackType.Buffon:
                JokerManager.instance.AddJoker(jokerData,false);
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
