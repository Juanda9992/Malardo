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
        currentCard = new Card(card);
        currentCard.linkedCard = this;
        card.SetCardChipAmmount();
        card.SetCardName();
        visuals.SetVisuals(currentCard);
        if (DebuffSuitBlind.suitDebuffed == currentCard.cardSuit)
        {
            DebuffCard();
            return;
        }

        if (IsNumberDebuffed(DebuffCustomCards.numbersDebuffed))
        {
            DebuffCard();
            return;
        }
    }

    [ContextMenu("TestVisuals")]
    private void TestCardVisuals()
    {
        visuals.SetVisuals(currentCard);
    }

    private void DisableCard()
    {
        currentCard.canPlay = false;
    }

    private void EnableCard()
    {
        currentCard.canPlay = true;
    }

    private void CheckForSuitDebuffed(Suit suit, bool status)
    {
        if (!status)
        {
            BuffCard();
        }
        if (currentCard.cardSuit == suit)
        {
            DebuffCard();
        }
        else
        {
            BuffCard();
        }
    }

    private void CheckForNumberDebuffed(int[] numbers)
    {
        if (IsNumberDebuffed(numbers))
        {
            DebuffCard();
        }
        else
        {
            BuffCard();
        }
    }

    private bool IsNumberDebuffed(int[] numbers)
    {
        if (numbers == null)
        {
            return false;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            if (currentCard.number == numbers[i])
            {
                return true;
            }
        }
        return false;
    }
    private void DebuffCard()
    {
        visuals.SetCardDisabled(true);
        DisableCard();
    }
    private void BuffCard()
    {
        visuals.SetCardDisabled(false);
        EnableCard();
    }

    void OnEnable()
    {
        DebuffSuitBlind.OnDebuffBlindStatus += CheckForSuitDebuffed;
        DebuffCustomCards.OnNumbersDebuffed += CheckForNumberDebuffed;
    }

    void OnDisable()
    {
        DebuffSuitBlind.OnDebuffBlindStatus -= CheckForSuitDebuffed;
        DebuffCustomCards.OnNumbersDebuffed -= CheckForNumberDebuffed;
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
    public bool canPlay = true;
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
    public Card(Card card = null)
    {
        if (card != null)
        {
            this.number = card.number;
            this.chipAmmount = card.chipAmmount;
            this.cardSuit = card.cardSuit;
            this.canPlay = card.canPlay;
            this.cardEdition = card.cardEdition;
            this.cardSeal = card.cardSeal;
            this.cardType = card.cardType;
            this.cardName = card.cardName;
            this.linkedCard = card.linkedCard;

            SetCardChipAmmount();
            SetCardName();
        }
    }

    private CardEdition GenerateRandomEdition()
    {
        int chanceGenerator = Random.Range(0, 100);

        if (chanceGenerator < 92)
        {
            return CardEdition.Base;
        }
        else if (chanceGenerator < 96)
        {
            return CardEdition.Foil;
        }
        else if (chanceGenerator < 98)
        {
            return CardEdition.Holographic;
        }
        else
        {
            return CardEdition.Polychrome;
        }

    }

    private CardType GetRandomCardType()
    {
        int generator = Random.Range(0, 100);
        CardType[] cardTypes = new CardType[] { CardType.Gold, CardType.Stone, CardType.Silver, CardType.Lucky, CardType.Glass, CardType.Bonus, CardType.Mult };


        if (generator < 30)
        {
            return cardTypes[Random.Range(0, cardTypes.Length)];
        }
        else
        {
            return CardType.Default;
        }
    }

    private Seal GenerateRandomSeal()
    {
        Seal[] allSeals = new Seal[] { Seal.Gold, Seal.Red, Seal.Blue, Seal.Purple };
        int generator = Random.Range(0, 100);
        if (generator < 20)
        {
            return allSeals[Random.Range(0, allSeals.Length)];
        }
        return Seal.None;
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
        if (cardType == CardType.Stone)
        {
            cardName = "Stone Card";
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
    Clover,
    None
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