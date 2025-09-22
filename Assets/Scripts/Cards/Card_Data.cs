using UnityEngine;
public class Card_Data : MonoBehaviour
{
    public Card currentCard;
    public CardVisuals visuals;
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
        currentCard.canPlay = true; 
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
        if (currentCard.cardSuit == suit || currentCard.cardType == CardType.Wild)
        {
            DebuffCard();
        }
        else
        {
            BuffCard();
        }
    }

    public void CheckForNumberDebuffed(int[] numbers)
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
    [Range(1, 14)] public int number;
    public int chipAmmount;
    public Suit cardSuit;
    public bool IsFaceCard { get { return (number >= 11 && number <= 13) || overrideFaceCard; } }
    public static bool overrideFaceCard;
    public bool IsSpecialCard { get { return cardType != CardType.Default; } }
    public CardType cardType = CardType.Default;
    public Seal cardSeal = Seal.None;
    public CardEdition cardEdition = CardEdition.Base;
    public bool canPlay = true;
    public int activations = 1;
    public int identifier;
    public Card GenerateRandomCard()
    {

        this.number = Random.Range(2, 14);
        this.cardSuit = CommonOperations.GetRandomSuit();
        this.cardType = CommonOperations.GetRandomCardType();
        this.cardSeal = CommonOperations.CalculateRandomSeal();
        this.cardEdition = CommonOperations.GenerateRandomCardEdition();

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
            this.activations = card.activations;

            SetCardChipAmmount();
            SetCardName();
        }
    }

    public void SetCardChipAmmount()
    {
        chipAmmount = number;
        if (number >= 11 && number <= 13)
        {
            chipAmmount = 10;
        }

        if (number == 14)
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
        cardName = number + " of " + GetFormatedCardSuit();

        if (number == 11)
        {
            cardName = "Jack of " + GetFormatedCardSuit();
        }
        else if (number == 12)
        {
            cardName = "Queen of " + GetFormatedCardSuit();
        }
        else if (number == 13)
        {
            cardName = "King of " + GetFormatedCardSuit();
        }
        else if (number == 14)
        {
            cardName = "Ace of " + GetFormatedCardSuit();
        }
        if (cardType == CardType.Stone)
        {
            cardName = "Stone Card";
        }
    }

    public void CopyCardData(Card card)
    {
        number = card.number;
        cardSuit = card.cardSuit;
        cardSeal = card.cardSeal;
        chipAmmount = card.chipAmmount;
        cardEdition = card.cardEdition;
        cardType = card.cardType;
        canPlay = card.canPlay;
        cardName = card.cardName;
    }

    private string GetFormatedCardSuit()
    {
        if (cardSuit == Suit.Hearth)
        {
            return "<style=Hearth>" + "Hearths" + "</style>";
        }
        else if (cardSuit == Suit.Diamond)
        {
            return "<style=Diamond>" + "Diamonds" + "</style>";
        }
        else if (cardSuit == Suit.Spades)
        {
            return "<style=Spade>" + "Spades" + "</style>";
        }
        else
        {
            return "<style=Club>" + "Clubs" + "</style>";
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
    Base = -1,
    Foil = 0,
    Holographic = 1,
    Polychrome = 2,
    Negative = 3
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
    Mult,
    Wild
}
