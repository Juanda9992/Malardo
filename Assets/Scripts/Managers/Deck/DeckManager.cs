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
        UpdateDeckCounter();
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
            int randomIndex = Random.Range(0, roundDeck.Count);
            Card cardToGenerate = roundDeck[randomIndex];
            roundDeck.RemoveAt(randomIndex);

            CardManager.instance.GenerateCardOnHand(cardToGenerate);
        }
    }

    public void RemoveCardFromDeck(Card card)
    {
        roundDeck.Remove(card);
    }

    private void UpdateDeckCounter()
    {
        deckCounter.text = roundDeck.Count + " / " + fullMatchDeck.Count;
    }
}
