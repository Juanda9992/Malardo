using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private int defaultHandSize = 8;
    [SerializeField] private CardGenerationPresset cardGenerationPresset;

    [SerializeField] private Transform handParent;
    [SerializeField] private GameObject cardprefab;

    [SerializeField] private TextMeshProUGUI deckCounter;
    public List<Card> cards;

    private int initialDeckSize;

    void Awake()
    {
        instance = this;
        cards = new List<Card>(cardGenerationPresset.allCards);
    }

    void Start()
    {
        initialDeckSize = cards.Count;

        for (int i = 0; i < defaultHandSize; i++)
        {
            GenerateCardOnHand();
        }
        UpdateDeckCounter();
    }

    public void AddCartToList(Card card)
    {
        cards.Add(card);
    }
    private void UpdateDeckCounter()
    {
        deckCounter.text = cards.Count + " / " + initialDeckSize; 
    }

    public void GenerateCardOnHand()
    {
        if (cards.Count == 0)
        {
            return;
        }
        Card card = cards[Random.Range(0, cards.Count)];

        cards.Remove(card);

        GameObject currentCard = Instantiate(cardprefab, handParent);

        currentCard.GetComponent<Card_Data>().SetCardData(card);
    }

    public void RemoveCardFromDeck(Card card)
    {
        cards.Remove(card);
        UpdateDeckCounter();

    }


}
