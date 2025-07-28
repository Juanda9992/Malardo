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
        
        this.number = Random.Range(1, 14);
        this.cardSuit = GetRandomCardSuit();
        this.cardType = GetRandomCardType();
        this.cardSeal = GenerateRandomSeal();
        this.cardEdition = GenerateRandomEdition();

        SetCardChipAmmount();
        SetCardName();

        return this;
    }

    private CardEdition GenerateRandomEdition()
    {
        CardEdition[] cardEditions = new CardEdition[] { CardEdition.Base, CardEdition.Foil, CardEdition.Holographic, CardEdition.Polychrome };

        return cardEditions[Random.Range(0, cardEditions.Length)];
    }

    private CardType GetRandomCardType()
    {
        CardType[] cardTypes = new CardType[] { CardType.Default, CardType.Gold, CardType.Stone, CardType.Silver, CardType.Lucky, CardType.Glass, CardType.Bonus, CardType.Mult };

        return cardTypes[Random.Range(0, cardTypes.Length)];
    }

    private Seal GenerateRandomSeal()
    {
        Seal[] allSeals = new Seal[] { Seal.None, Seal.Gold, Seal.Red, Seal.Blue, Seal.Purple };

        return allSeals[Random.Range(0, allSeals.Length)];
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