using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class HandDetector : MonoBehaviour
{
    [SerializeField] private List<Card> cardsSorted;
    void Start()
    {
        HandManager.instance.OnHandChanged += DetectHandPlayed;
    }

    private void DetectHandPlayed(List<Card> cards)
    {
        if (cards.Count == 0)
        {
            return;
        }
        CheckIfColor(cards);
        CheckIfStraight(cards);
        CheckIfFourOfAkind(cards);
    }

    private bool CheckIfFourOfAkind(List<Card> cards)
    {
        if (cards.Count < 4)
        {
            return false;
        }

        List<Card> validCards;

        validCards = GetCardsInHandByNumber(cards, cards[0].number); 
        if (validCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            return true;
        }

        validCards = GetCardsInHandByNumber(cards, cards[3].number);
        if (validCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            return true;
        }
        return false;
    }
    private bool CheckIfStraight(List<Card> cards)
    {
        if (cards.Count != 5)
        {
            cardsSorted.Clear();
            return false;
        }

        cardsSorted = new List<Card>(cards);
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
    private bool CheckIfColor(List<Card> cards)
    {
        if (cards.Count != 5)
        {
            return false;
        }
        Suit currentSuit = cards[0].cardSuit;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].cardSuit != currentSuit)
            {
                return false;
            }
        }

        Debug.Log("Suit");
        return true;
    }

    private List<Card> GetCardsInHandByNumber(List<Card> cards, int predicate)
    {

        List<Card> validCards = cards.FindAll(x => x.number == predicate); 
        Debug.Log(validCards.Count);
        return validCards;
    }
}
