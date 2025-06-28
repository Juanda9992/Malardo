using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class HandDetector : MonoBehaviour
{
    public static HandDetector instance;
    [SerializeField] private List<Card> cardsSorted;
    [SerializeField] private List<Card> realCards;
    [SerializeField] private List<Card> handCards;

    [SerializeField] private List<HandData> allHands;

    [SerializeField] private TextMeshProUGUI handNameText;
    private HandData currentHand;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        HandManager.instance.OnHandChanged += DetectHandPlayed;
        RemoveHandFromMult();
    }

    private void DetectHandPlayed(List<Card> cards)
    {
        handCards = new List<Card>(cards);
        realCards = new List<Card>(cards);
        Debug.Log(handCards.Count);
        if (handCards.Count == 0)
        {
            RemoveHandFromMult();
            cardsSorted.Clear();
            return;
        }
        if (CheckIfFiveOfAKind())
        {
            AddHandToMult();
            return;
        }
        if (CheckIfColor())
        {
            AddHandToMult();
            return;
        }

        if (CheckIfStraight())
        {
            AddHandToMult();
            return;
        }

        if (CheckIfFourOfAkind())
        {
            AddHandToMult();
            return;
        }

        if (CheckIfFullHouse())
        {
            AddHandToMult();
            return;
        }
        Debug.Log("Enter with" + handCards.Count);

        if (CheckIfDoublePair())
        {
            AddHandToMult();
            return;
        }

        if (CheckIfThreeOfAKind())
        {
            AddHandToMult();
            return;
        }

        if (CheckIfPair())
        {
            AddHandToMult();
            return;
        }

        SetHighCard();
    }

    private void AddHandToMult()
    {
        handNameText.text = currentHand.name;
        ScoreManager.instance.SetChips(currentHand.baseChips);
        ScoreManager.instance.SetMult(currentHand.baseMult);
    }

    public void RemoveHandFromMult()
    {
        handNameText.text = "";
        ScoreManager.instance.ResetChipsAndMult();
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
                currentHand = allHands.Find(x => x.handType == HandType.Straight);
                Debug.Log("Straight");
                CardPlayer.instance.ReceiveHandCards(cardsSorted);
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
        currentHand = allHands.Find(x => x.handType == HandType.Flush);
        CardPlayer.instance.ReceiveHandCards(handCards);
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
            CardPlayer.instance.ReceiveHandCards(handCards);
            currentHand = allHands.Find(x => x.handType == HandType.Five_Of_A_Kind);
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

        realCards = GetCardsInHandByNumber(handCards, handCards[0].number);
        if (realCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            CardPlayer.instance.ReceiveHandCards(realCards);
            return true;
        }

        realCards = GetCardsInHandByNumber(handCards, handCards[3].number);
        if (realCards.Count == 4)
        {
            Debug.Log("Four of a Kind");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = allHands.Find(x => x.handType == HandType.Four_Of_A_Kind);
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
            realCards = GetCardsInHandByNumber(handCards, handCards[i].number);
            if (realCards.Count == 3)
            {
                CardPlayer.instance.ReceiveHandCards(realCards);
                Debug.Log("Three of a kind");
                currentHand = allHands.Find(x => x.handType == HandType.Three_Of_A_Kind);
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

        realCards = new List<Card>();
        int pairsFound = 0;

        for (int i = 0; i < handCards.Count; i++)
        {
            if (GetCardsInHandByNumber(handCards, handCards[i].number).Count == 2)
            {
                pairsFound++;
                realCards.Add(handCards[i]);
            }
        }

        if (pairsFound == 4)
        {
            Debug.Log("Double pair");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = allHands.Find(x => x.handType == HandType.Double_Pair);
        }
        return pairsFound == 4;
    }
    private bool CheckIfFullHouse()
    {
        if (handCards.Count < 5)
        {
            return false;
        }

        realCards = new List<Card>(handCards);
        int matches = 0;
        int match1 = 0;

        bool pair = false;
        bool three = false;
        for (int i = realCards.Count - 1; i >= 0; i--)
        {
            if (realCards[i].number == match1)
            {
                continue;
            }
            if (GetCardsInHandByNumber(realCards, realCards[i].number).Count == 2 && !pair)
            {
                pair = true;
                match1 = realCards[i].number;
                matches++;
                continue;
            }

            if (GetCardsInHandByNumber(realCards, realCards[i].number).Count == 3 && !three)
            {
                three = true;
                match1 = realCards[i].number;
                matches++;
                continue;
            }
        }

        if (matches == 2)
        {
            Debug.Log("Full House");
            CardPlayer.instance.ReceiveHandCards(handCards);
            currentHand = allHands.Find(x => x.handType == HandType.Full_House);
        }

        return matches == 2;
    }
    private bool CheckIfPair()
    {
        if (handCards.Count < 2)
        {
            return false;
        }

        int matches = 0;

        realCards = new List<Card>();
        for (int i = 0; i < handCards.Count; i++)
        {
            if (GetCardsInHandByNumber(handCards, handCards[i].number).Count == 2)
            {
                realCards.Add(handCards[i]);
                matches++;
            }
        }

        if (matches == 2)
        {
            Debug.Log("Pair");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = allHands.Find(x => x.handType == HandType.Pair);
        }
        return matches == 2;
    }
    private void SetHighCard()
    {
        if (handCards.Count > 0)
        {
            realCards = realCards.OrderBy(x => x.number).ToList();
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = allHands.Find(x => x.handType == HandType.High_Card);
            AddHandToMult();
        }
    }
    private List<Card> GetCardsInHandByNumber(List<Card> cards, int predicate)
    {
        List<Card> validCards = cards.FindAll(x => x.number == predicate);
        return validCards;
    }
}
