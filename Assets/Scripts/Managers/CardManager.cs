using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    public List<Card> cards;

    void Awake()
    {
        instance = this;
        cards = new List<Card>();
    }

    public void AddCartToList(Card card)
    {
        cards.Add(card);
    }
}
