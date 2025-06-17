using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;
    public List<Card> handCards;
    [SerializeField] private int handLimit = 5;
    public bool CanAddCards { get { return handCards.Count < handLimit; } }

    [SerializeField] private InputActionReference rightClick;
    void Awake()
    {
        instance = this;
        handCards = new List<Card>();
    }

    void Start()
    {
        rightClick.action.performed += _ => RemoveAllCards();
    }

    public void AddCardToHand(Card card)
    {
        handCards.Add(card);
    }

    public void RemoveCardFromHand(Card card)
    {
        handCards.Remove(card);
    }

    public void RemoveAllCards()
    {
        if (handCards.Count > 0)
        {
            for (int i = handCards.Count-1; i >= 0; i--)
            {
                handCards[i].linkedCard.pointerInteraction.UnSelect();
            }
        }
    }
}
