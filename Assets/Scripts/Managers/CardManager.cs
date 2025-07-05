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
    public List<Card_Data> cardsOnScreen;

    private int initialDeckSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        GenerateCardsCoroutine();
        BlindManager.instance.OnBlindDefeated += GenerateCardsCoroutine;
    }
    private IEnumerator GenerateCards()
    {
        TryDestroyExistingCards();
        yield return new WaitForSeconds(0.1f);
        cards = new List<Card>(cardGenerationPresset.allCards);
        cardsOnScreen = new List<Card_Data>();
        initialDeckSize = cards.Count;
        for (int i = 0; i < defaultHandSize; i++)
        {
            GenerateCardOnHand();
        }
        UpdateDeckCounter();

    }

    private void GenerateCardsCoroutine()
    {
        StartCoroutine(nameof(GenerateCards));
    }

    private void TryDestroyExistingCards()
    {
        if (handParent.childCount == 0)
        {
            return;
        }
        CardPointerInteraction[] handChilds = handParent.GetComponentsInChildren<CardPointerInteraction>();
        for (int i = 0; i < handChilds.Length; i++)
        {
            handChilds[i].DestroyCard();
        }
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
        cardsOnScreen.Add(currentCard.GetComponent<Card_Data>());
    }

    public void RemoveCardFromDeck(Card card)
    {
        cards.Remove(card);
        cardsOnScreen.Remove(card.linkedCard);
        UpdateDeckCounter();

    }


}
