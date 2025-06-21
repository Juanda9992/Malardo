using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class HandDetector : MonoBehaviour
{
    [SerializeField] private List<Card> cardsSorted;
    [SerializeField] private List<Card> handCards;
    void Start()
    {
        HandManager.instance.OnHandChanged += DetectHandPlayed;
    }

    private void DetectHandPlayed(List<Card> cards)
    {
        handCards = new List<Card>(cards);
        if (handCards.Count == 0)
        {
            return;
        }

        if (CheckIfFiveOfAKind())
        {
            return;
        }
        if (CheckIfColor())
        {
            return;
        }

        if (CheckIfStraight())
        {
            return;
        }

        if (CheckIfFourOfAkind())
        {
            return;
        }

        if (CheckIfFullHouse())
        {
            return;
        }

        if (CheckIfDoublePair())
        {
            return;
        }

        if (CheckIfThreeOfAKind())
        {
            return;
        }

        if (CheckIfPair())
        {
            return;
        }

        SetHighCard();
    }
    private bool CheckIfStraight()
    {
        if (handCards.Count != 5)
        {
            cardsSorted.Clear();
            return false;
        }
        cardsSorted = new List<Card>(handCards);
        cardsSorted = cardsSorted.OrderBy(x => x.number).ToList();

        Card nextCard;
        for (int i = 0; i < cardsSorted.Count; i++)
        {
            nextCard = i + 1 < cardsSorted.Count ? cardsSorted[i + 1] : null;
            if (nextCard != null)
            {
                if (Mathf.Abs(cardsSorted[i].number - nextCard.number) != 1)
                {
                    return false;
                }
            }
            if (Mathf.Abs(cardsSorted[3].number - cardsSorted[4].number) == 1)
            {
                Debug.Log("Straight");
                return true;
            }
        }
        return true;
    }
    private bool CheckIfColor()
    {
        if (handCards.Count != 5)
        {
            return false;
        }
        Suit currentSuit = handCards[0].cardSuit;
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].cardSuit != currentSuit)
            {
                return false;
            }
        }

        Debug.Log("Suit");
        return true;
    }
    private bool CheckIfFiveOfAKind()
    {
        if (handCards.Count < 5)
        {
            return false;
        }

        if (GetCardsInHandByNumber(handCards, handCards[0].number).Count == 5)
        {
            Debug.Log("Five of a kind");
            return true;
        }

        return false;
    }
    private bool CheckIfFourOfAkind()
    {
        if (handCards.Count < 4)
        {
            return false;
        }

        List<Card> validCards;

        validCards = GetCardsInHandByNumber(handCards, handCards[0].number);
        if (validCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            return true;
        }

        validCards = GetCardsInHandByNumber(handCards, handCards[3].number);
        if (validCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            return true;
        }
        return false;
    }
    private bool CheckIfThreeOfAKind()
    {
        if (handCards.Count < 3)
        {
            return false;
        }

        for (int i = 0; i < handCards.Count; i++)
        {
            if (GetCardsInHandByNumber(handCards, handCards[i].number).Count == 3)
            {
                Debug.Log("Three of a kind");
                return true;
            }
        }
        return false;
    }
    private bool CheckIfDoublePair()
    {
        if (handCards.Count < 4)
        {
            return false;
        }

        List<Card> referenceList = new List<Card>(handCards);
        int pairsFound = 0;
        int match1 = 0;
        for (int i = referenceList.Count - 1; i >= 0; i--)
        {
            if (referenceList[i].number == match1)
            {
                continue;
            }
            if (GetCardsInHandByNumber(referenceList, referenceList[i].number).Count == 2)
            {
                match1 = referenceList[i].number;
                pairsFound++;
            }
        }

        if (pairsFound == 2)
        {
            Debug.Log("Double pair");
        }
        return pairsFound == 2;
    }
    private bool CheckIfFullHouse()
    {
        if (handCards.Count < 5)
        {
            return false;
        }

        List<Card> referenceList = new List<Card>(handCards);
        int matches = 0;
        int match1 = 0;

        bool pair = false;
        bool three = false;
        for (int i = referenceList.Count - 1; i >= 0; i--)
        {
            if (referenceList[i].number == match1)
            {
                continue;
            }
            if (GetCardsInHandByNumber(referenceList, referenceList[i].number).Count == 2 && !pair)
            {
                pair = true;
                match1 = referenceList[i].number;
                matches++;
                continue;
            }

            if (GetCardsInHandByNumber(referenceList, referenceList[i].number).Count == 3 && !three)
            {
                three = true;
                match1 = referenceList[i].number;
                matches++;
                continue;
            }
        }

        if (matches == 2)
        {
            Debug.Log("Full House");
        }

        return true;
    }
    private bool CheckIfPair()
    {
        if (handCards.Count < 2)
        {
            return false;
        }

        int match0 = 0;
        int matches = 0;
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].number == match0)
            {
                continue;
            }
            if (GetCardsInHandByNumber(handCards, handCards[i].number).Count == 2)
            {
                matches++;
                match0 = handCards[i].number;
            }
        }

        if (matches == 1)
        {
            Debug.Log("Pair");
        }
        return matches == 1;
    }
    private void SetHighCard()
    {
        if (handCards.Count > 0)
        {
            Debug.Log("High Card");
        }
    }
    private List<Card> GetCardsInHandByNumber(List<Card> cards, int predicate)
    {
        List<Card> validCards = cards.FindAll(x => x.number == predicate);
        return validCards;
    }
}
