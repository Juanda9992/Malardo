using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;
    public List<Card> handCards;
    [SerializeField] private int handLimit = 5;
    public bool CanAddCards { get { return handCards.Count < handLimit; } }
    void Awake()
    {
        instance = this;
        handCards = new List<Card>();
    }

    public void AddCardToHand(Card card)
    {
        handCards.Add(card);
    }
    
    public void RemoveCardFromHand(Card card)
    {
        handCards.Remove(card);
    }
}
