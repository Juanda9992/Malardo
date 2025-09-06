using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance;

    public CardGenerationPresset referenceDeck;

    public List<Card> fullMatchDeck;

    public List<Card> roundDeck;
    public int currentHandSize = 8;
    public int initialDeckSize;
    public int roundDeckSize;
    public int cardIndex = 0;
    [SerializeField] private TextMeshProUGUI deckCounter;

    void Awake()
    {
        instance = this;

        fullMatchDeck = new List<Card>();
        for (int i = 0; i < referenceDeck.allCards.Count; i++)
        {
            Card newCard = new Card(referenceDeck.allCards[i]);
            fullMatchDeck.Add(newCard);
            fullMatchDeck[i].identifier = cardIndex;
            cardIndex++;
        }
        initialDeckSize = fullMatchDeck.Count;

    }
    void Start()
    {
        GameEventsManager.instance.OnRoundBegins += OnRoundStart;
    }

    private void OnRoundStart()
    {
        roundDeckSize = fullMatchDeck.Count;
        RegenerateRoundDeck();
        RegenerateDeck();
    }

    public void RegenerateRoundDeck()
    {
        roundDeck = new List<Card>(fullMatchDeck);
    }

    public List<Card> GetRoundDeck()
    {
        return roundDeck;
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
        StartCoroutine("SetHandSpacing");
    }

    private IEnumerator SetHandSpacing()
    {
        yield return new WaitForSeconds(0.2f);
        CardManager.instance.SetHandSpacing();
    }

    public void CreateRandomCard()
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

    public void AddCardOnFullDeck(Card card)
    {
        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnCardAdded));
        fullMatchDeck.Add(card);
        UpdateDeckCounter();
    }
    public void DestroyCardFromFullDeck(Card card)
    {
        fullMatchDeck.Remove(card);
        UpdateDeckCounter();
    }

    public void AddHandSize(int ammount)
    {
        currentHandSize += ammount;
        CardManager.instance.TrimDeck(currentHandSize);
    }

    [ContextMenu("Add 1 hands size")]
    private void Add1HandSize()
    {
        AddHandSize(+1);
    }
    [ContextMenu("Substract 1 hands size")]
    private void Substract1HandSize()
    {
        AddHandSize(-1);
    }

    public void UpdateCardOnFullDeck(Card oldCard, Card newCard)
    {
        int index = fullMatchDeck.IndexOf(oldCard);
        Debug.Log(fullMatchDeck.Find(x => x == oldCard));
        //fullMatchDeck[index] = newCard;
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

    public int GetAllSilverCardsInDeck()
    {
        return fullMatchDeck.FindAll(x => x.cardType == CardType.Silver).Count;
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
        return fullMatchDeck.Find(x => x == card);
    }

    public int GetNumberOfSpecialCardsInDeck()
    {
        return fullMatchDeck.FindAll(x => x.cardType != CardType.Default).Count;
    }
    #endregion
}
