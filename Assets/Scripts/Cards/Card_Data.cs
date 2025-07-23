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
        card.SetCardName();
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
    public string cardName;
    public Card_Data linkedCard;
    [Range(1, 13)] public int number;
    public int chipAmmount;
    public Suit cardSuit;
    public bool IsFaceCard { get { return number >= 11 && number <= 13; } }
    public bool IsSpecialCard { get { return cardType != CardType.Default; } }
    public CardType cardType = CardType.Default;
    public Seal cardSeal = Seal.None;
    public CardEdition cardEdition = CardEdition.Base;
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

    public void SetCardName()
    {
        cardName = number + " of " + cardSuit.ToString();

        if (number == 11)
        {
            cardName = "Jack of " + cardSuit.ToString();
        }
        else if (number == 12)
        {
            cardName = "Queen of " + cardSuit.ToString();
        }
        else if (number == 13)
        {
            cardName = "King of " + cardSuit.ToString();
        }
        else if (number == 1)
        {
            cardName = "Ace of " + cardSuit.ToString();
        }
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
public enum Seal
{
    None,
    Gold,
    Red,
    Blue,
    Purple
}

public enum CardEdition
{
    Base,
    Foil,
    Holographic,
    Polychrome
}

public enum CardType
{
    Default,
    Gold,
    Stone,
    Silver,
    Lucky,
    Glass,
    Bonus,
    Mult
}