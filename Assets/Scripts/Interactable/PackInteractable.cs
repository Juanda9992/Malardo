using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackInteractable : MonoBehaviour
{
    public PackType itemType;
    public JokerData jokerData;
    public PackData packData;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Button actionButton;
    private Card createdCard;
    private PlanetCardData _planetCardData;
    public void SetJokerInfo(JokerInstance createdJoker)
    {
        jokerData = createdJoker.data;
        itemType = PackType.Buffon;
        GetComponent<DescriptionContainer>().SetNameAndDescription(jokerData.jokerName, jokerData.description);
        cardName.text = createdJoker.data.jokerName;
        ListenForAvaliability();
    }

    public void SetPackCard()
    {
        itemType = PackType.Card;
        createdCard = new Card();
        createdCard = createdCard.GenerateRandomCard();

        GetComponent<CardVisuals>().SetVisuals(createdCard);
        GetComponent<DescriptionContainer>().SetNameAndDescription(createdCard.cardName, "+" + createdCard.chipAmmount + " chips");
    }

    public void SetPlanetCard(PlanetCardData planetCardData)
    {
        _planetCardData = planetCardData;
        itemType = PackType.Planet;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(_planetCardData.handType);
        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        cardName.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc);
    }

    public void ListenForAvaliability()
    {
        if (itemType == PackType.Buffon)
        {
            actionButton.interactable = JokerManager.instance.CanAddJoker();
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
                break;
        }

        PackManager.instance.SelectItem();
    }
}
