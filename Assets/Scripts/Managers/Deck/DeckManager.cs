using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance;

    public CardGenerationPresset referenceDeck;

    public List<Card> fullMatchDeck;

    public List<Card> roundDeck;
    public int currentHandSize = 8;
    [SerializeField] private TextMeshProUGUI deckCounter;

    void Awake()
    {
        instance = this;
        fullMatchDeck = new List<Card>(referenceDeck.allCards);
    }
    void Start()
    {
        OnRoundStart();
        RegenerateDeck();
    }

    private void OnRoundStart()
    {
        roundDeck = new List<Card>(fullMatchDeck);
    }

    public void RegenerateDeck()
    {
        for (int i = 0; i < currentHandSize; i++)
        {
            CreateRandomCard();
        }
    }

    public void GenerateCardsOnDeck(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            CreateRandomCard();
        }
    }

    private void CreateRandomCard()
    {
        if (roundDeck.Count == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, roundDeck.Count);
        Card cardToGenerate = roundDeck[randomIndex];
        roundDeck.RemoveAt(randomIndex);

        CardManager.instance.GenerateCardOnHand(cardToGenerate);
        UpdateDeckCounter();
    }

    public void RemoveCardFromDeck(Card card)
    {
        roundDeck.Remove(card);
    }

    private void UpdateDeckCounter()
    {
        deckCounter.text = roundDeck.Count + " / " + fullMatchDeck.Count;
    }
    public void DestroyCardFromFullDeck(Card card)
    {
        fullMatchDeck.Remove(card);
    }

    #region GetValues
    public int GerRemainingCardOnDeck()
    {
        return roundDeck.Count;
    }

    public int GetAllCardsOnFullDeckByNumber(int numberRequired)
    {
        return fullMatchDeck.FindAll(x => x.number == numberRequired).Count;
    }

    public int GetAllCardsOnFullDeckBySuit(Suit suitRequired)
    {
        return fullMatchDeck.FindAll(x => x.cardSuit == suitRequired).Count;
    }

    public int GetAllStoneCardsOnFullDeck()
    {
        return fullMatchDeck.FindAll(x => x.cardType == CardType.Stone).Count;
    }

    public int GetRemainingCardsOnMatchDeck()
    {
        return roundDeck.Count;
    }

    public int GetRemainingCardsOnFullDeck()
    {
        return fullMatchDeck.Count;
    }

    public Card GetFullDeckCard(Card card)
    {
        return fullMatchDeck.Find(x=> x == card);
    }
    #endregion
}
