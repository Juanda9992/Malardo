using UnityEngine;

public class Card_Data : MonoBehaviour
{
    public Card currentCard;
    [ContextMenu("Generate Card")]
    private void GenerateRandomCard()
    {
        currentCard = currentCard.GenerateRandomCard();
    }


}

[System.Serializable]
public class Card
{
    [Range(1, 12)] public int number;
    public Suit cardSuit;
    public FaceCard faceCard = FaceCard.None;

    public Card GenerateRandomCard()
    {
        Card card = new Card();

        card.number = Random.Range(1, 12);
        card.cardSuit = GetRandomCardSuit();
        card.faceCard = GetRandomFaceCard(card);

        return card;
    }

    private Suit GetRandomCardSuit()
    {
        Suit[] cards = new Suit[] { Suit.Diamond, Suit.Clover, Suit.Hearth, Suit.Spades };

        return (cards[Random.Range(0, cards.Length)]);
    }

    private FaceCard GetRandomFaceCard(Card card)
    {
        if (card.number == 11)
        {
            FaceCard[] faceCards = new FaceCard[] { FaceCard.King, FaceCard.Queen, FaceCard.Joker };
            return (faceCards[Random.Range(0, faceCards.Length)]);
        }
        return FaceCard.None;
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
