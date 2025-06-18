using System;
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

    public event Action<List<Card>> OnHandPlayed;

    [SerializeField] private GameObject playButton, discardButton;

    [SerializeField] private InputActionReference rightClick;
    void Awake()
    {
        instance = this;
        handCards = new List<Card>();
    }

    void Start()
    {
        rightClick.action.performed += _ => RemoveAllCards();
        SetPlayButtonsState(false);
    }

    public void AddCardToHand(Card card)
    {
        handCards.Add(card);
        SetPlayButtonsState(true);
    }

    public void RemoveCardFromHand(Card card)
    {
        handCards.Remove(card);

        SetPlayButtonsState(handCards.Count > 0);
    }

    public void RemoveAllCards()
    {
        if (handCards.Count > 0)
        {
            for (int i = handCards.Count - 1; i >= 0; i--)
            {
                handCards[i].linkedCard.pointerInteraction.UnSelect();
            }
        }
        SetPlayButtonsState(false);
    }

    public void DiscardAllCards()
    {
        if (handCards.Count > 0)
        {
            for (int i = handCards.Count - 1; i >= 0; i--)
            {
                handCards[i].linkedCard.pointerInteraction.DiscardCard();
            }
        }

        handCards.Clear();
        SetPlayButtonsState(false);
    }

    private void SetPlayButtonsState(bool active)
    {
        playButton.SetActive(active);
        discardButton.SetActive(active);
    }

    public void PlayHand()
    {
        OnHandPlayed?.Invoke(handCards);
    }
}
