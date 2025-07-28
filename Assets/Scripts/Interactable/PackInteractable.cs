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
    public void SetJokerInfo(JokerData createdJoker)
    {
        jokerData = createdJoker;
        itemType = PackType.Buffon;
        GetComponent<DescriptionContainer>().SetNameAndDescription(jokerData.jokerName, jokerData.description);
        cardName.text = createdJoker.jokerName;
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
        }

        PackManager.instance.SelectItem();
    }
}
