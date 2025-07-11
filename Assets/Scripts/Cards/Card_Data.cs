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
    public Suit cardSuit;
    public FaceCard faceCard = FaceCard.None;

    public Card GenerateRandomCard()
    {
        Card card = new Card();

        card.number = Random.Range(1, 14);
        card.cardSuit = GetRandomCardSuit();
        SetCardFace(card);

        return card;
    }

    private Suit GetRandomCardSuit()
    {
        Suit[] cards = new Suit[] { Suit.Diamond, Suit.Clover, Suit.Hearth, Suit.Spades };

        return (cards[Random.Range(0, cards.Length)]);
    }

    public void SetCardNumber(int _number)
    {
        number = _number;
    }

    public void SetCardFace(Card card)
    {
        if (card.number == 11)
        {
            card.faceCard = FaceCard.Joker;
        }
        else if (card.number == 12)
        {
            card.faceCard = FaceCard.Queen;
        }
        else if (card.number == 13)
        {
            card.faceCard = FaceCard.King;
        }
        else
        {
            card.faceCard = FaceCard.None;
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

public enum FaceCard
{
    None,
    King,
    Queen,
    Joker
}
