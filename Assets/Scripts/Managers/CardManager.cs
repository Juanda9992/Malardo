using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] private int defaultHandSize = 8;
    [SerializeField] private CardGenerationPresset cardGenerationPresset;

    [SerializeField] private Transform handParent;
    [SerializeField] private GameObject cardprefab;
    public List<Card> cards;

    void Awake()
    {
        instance = this;
        cards = new List<Card>();

        cards = cardGenerationPresset.allCards;
    }

    void Start()
    {
        for (int i = 0; i < defaultHandSize; i++)
        {
            GenerateCardOnHand();
        }
    }

    public void AddCartToList(Card card)
    {
        cards.Add(card);
    }

    public void GenerateCardOnHand()
    {
        Card card = cards[Random.Range(0, cards.Count)];

        cards.Remove(card);

        GameObject currentCard = Instantiate(cardprefab, handParent);

        currentCard.GetComponent<Card_Data>().SetCardData(card);
    }




}
