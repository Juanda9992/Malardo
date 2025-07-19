using UnityEngine;

public class Card_Data : MonoBehaviour
{

    public Card currentCard;
    [SerializeField] private CardVisuals visuals;
    public CardPointerInteraction pointerInteraction;
    [ContextMenu("Generate Card")]
    private void GenerateRandomCard()
    {
        currentCard = currentCard.GenerateRandomCard();
        currentCard.linkedCard = this;
        visuals.SetVisuals(currentCard);
    }

    public void SetCardData(Card card)
    {
        currentCard = card;
        currentCard.linkedCard = this;
        card.SetCardChipAmmount();
        visuals.SetVisuals(currentCard);
    }

    [ContextMenu("TestVisuals")]
    private void TestCardVisuals()
    {
        visuals.SetVisuals(currentCard);
    }
}

[System.Serializable]
public class Card
{
    public Card_Data linkedCard;
    [Range(1, 13)] public int number;
    public int chipAmmount;
    public Suit cardSuit;
    public bool IsFaceCard {get { return number >= 11 && number <= 13; }}
    public bool IsSpecialCard {get { return cardType == CardType.Gold || cardType == CardType.Stone; }}
    public CardType cardType = CardType.Default;
    public Card GenerateRandomCard()
    {
        Card card = new Card();

        card.number = Random.Range(1, 14);
        card.cardSuit = GetRandomCardSuit();

        return card;
    }

    private Suit GetRandomCardSuit()
    {
        Suit[] cards = new Suit[] { Suit.Diamond, Suit.Clover, Suit.Hearth, Suit.Spades };

        return (cards[Random.Range(0, cards.Length)]);
    }

    public void SetCardChipAmmount()
    {
        chipAmmount = number;
        if (number >= 11 && number < 13)
        {
            chipAmmount = 10;
        }

        if (number == 1)
        {
            chipAmmount = 11;
        }

        if (cardType == CardType.Stone)
        {
            chipAmmount = 50;
        }
    }
    public void SetCardNumber(int _number)
    {
        number = _number;
    }

    public void DegubCardInfo()
    {
        Debug.Log($"Number {number}, Suit {cardSuit}");
    }
}

public enum Suit
{
    Diamond,
    Hearth,
    Spades,
    Clover
}

public enum CardType
{
    Default,
    Gold,
    Stone
}