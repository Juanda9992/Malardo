using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public int defaultDiscards = 3;
    [SerializeField] private int discards;
    public int defaultHands = 4;
    [SerializeField] private int hands;
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
    }

    IEnumerator Start()
    {
        rightClick.action.performed += _ => RemoveAllCards();
        SetPlayButtonsState(false);

        BlindManager.instance.OnBlindDefeated += ResetHandsAndDiscards;


        yield return new WaitForSeconds(0.5f);

        hands = defaultHands;
        discards = defaultDiscards;
        UpdateDiscardText();
        UpdateHandText();


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
        if (CardPlayer.instance.isPlayingCards) return;
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
        GameStatusManager.SetDiscardData(handCards);
        GameStatusManager.SetGameEvent(TriggerOptions.CardDiscard);
        if (handCards.Count > 0)
        {
            ClearHandPlayed();
        }

        GameStatusManager.SetDiscardsRemaining(discards);

        SetPlayButtonsState(false);
        GameEventsManager.instance.TriggerHandDiscard();
        UpdateDiscardText();
    }

    public void ClearHandPlayed()
    {
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].linkedCard != null)
            {
                handCards[i].linkedCard.pointerInteraction.DestroyCard();
                CardManager.instance.cardsOnScreen.Remove(handCards[i].linkedCard);
            }
        }
        DeckManager.instance.GenerateCardsOnDeck(handCards.Count);
        handCards.Clear();
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
        GameStatusManager.SetGameEvent(TriggerOptions.BeforeHandPlay);
        GameStatusManager.SetHandsRemaining(hands);
        OnHandPlayed?.Invoke(handCards);
        GameEventsManager.instance.TriggerHandPlayed();
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
        Debug.Log("Reset");
        hands = defaultHands;
        discards = defaultDiscards;
        GameStatusManager._Status.discardData.discardsOnMatch = 0;

        UpdateDiscardText();
        UpdateHandText();
    }

    public void SetDefaultHands(int ammount, bool matchHands = false)
    {
        defaultHands = ammount;
        if (hands > defaultHands)
        {
            hands = defaultHands;
        }

        if (matchHands)
        {
            hands = ammount;
        }
        UpdateHandText();
    }

    public void SetDefaultDiscards(int ammount, bool matchDiscards = false)
    {
        defaultDiscards = ammount;

        if (discards > defaultDiscards)
        {
            discards = defaultDiscards;
        }
        if (matchDiscards)
        {
            discards = ammount;
        }
        UpdateDiscardText();
    }

    public int GetHandsRemaining()
    {
        return hands;
    }
}
