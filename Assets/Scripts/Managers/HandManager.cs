using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int discards = 3;
    public int hands = 4;
    public static HandManager instance;
    public List<Card> handCards;
    [SerializeField] private int handLimit = 5;
    public bool CanAddCards { get { return handCards.Count < handLimit; } }

    public event Action<List<Card>> OnHandPlayed;

    [SerializeField] private Button playButton, discardButton;

    [SerializeField] private InputActionReference rightClick;

    [SerializeField] private TextMeshProUGUI discardsText, handsText;

    public event Action<List<Card>> OnHandChanged;
    void Awake()
    {
        instance = this;
        handCards = new List<Card>();

        UpdateDiscardText();
        UpdateHandText();
    }

    void Start()
    {
        rightClick.action.performed += _ => RemoveAllCards();
        SetPlayButtonsState(false);

        BlindManager.instance.OnBlindDefeated += ResetHandsAndDiscards;

        GameStatusManager.SetHandsRemaining(hands);
        GameStatusManager.SetDiscardsRemaining(discards);
    }

    public void AddCardToHand(Card card)
    {
        handCards.Add(card);
        OnHandChanged?.Invoke(handCards);
        SetPlayButtonsState(true);
    }

    public void RemoveCardFromHand(Card card)
    {
        handCards.Remove(card);
        OnHandChanged?.Invoke(handCards);
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
        discards--;
        int requiredCards = handCards.Count;
        if (handCards.Count > 0)
        {
            for (int i = handCards.Count - 1; i >= 0; i--)
            {
                handCards[i].linkedCard.pointerInteraction.DestroyCard();
                CardManager.instance.RemoveCardFromDeck(handCards[i]);
                GameStatusManager.SetGameEvent(TriggerOptions.CardDiscard);
            }
        }

        handCards.Clear();

        GameStatusManager.SetDiscardsRemaining(discards);

        CardManager.instance.TryGenerateCardsOnHand(requiredCards);

        SetPlayButtonsState(false);
        GameEventsManager.instance.TriggerHandDiscard();
        UpdateDiscardText();
    }

    public IEnumerator ClearHandPlayed()
    {
        int requiredHandNumber = handCards.Count;
        for (int i = 0; i < handCards.Count; i++)
        {
            handCards[i].linkedCard.pointerInteraction.DestroyCard();
        }
        handCards.Clear();

        yield return new WaitForSeconds(0.2f);

        CardManager.instance.TryGenerateCardsOnHand(requiredHandNumber);
    }

    private void SetPlayButtonsState(bool active)
    {
        playButton.gameObject.SetActive(active);

        discardButton.gameObject.SetActive(active);
    }

    private void UpdateDiscardText()
    {
        discardButton.interactable = discards > 0;
        discardsText.text = discards.ToString();
    }

    private void UpdateHandText()
    {
        playButton.interactable = hands > 0;
        handsText.text = hands.ToString();
    }

    public void PlayHand()
    {
        hands--;
        OnHandPlayed?.Invoke(handCards);
        GameEventsManager.instance.TriggerHandPlayed();
        GameStatusManager.SetGameEvent(TriggerOptions.BeforeHandPlay);
        SetPlayButtonsState(false);
        UpdateHandText();
    }

    public void UpdateHandCardsPosition()
    {
        foreach (var card in handCards)
        {
            card.linkedCard.pointerInteraction.UpdateCardPos();
        }
    }

    private void ResetHandsAndDiscards()
    {
        hands = 4;
        discards = 3;

        UpdateDiscardText();
        UpdateHandText();
    }
}
