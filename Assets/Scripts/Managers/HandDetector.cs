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
    public PokerHandLevelData currentHand;
    public int requiredAmmountForFlush = 5;
    public int gapForStraights = 1;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        HandManager.instance.OnHandChanged += DetectHandPlayed;
        GameEventsManager.instance.OnHandEnd += ResetValues;
        RemoveHandFromMult();
    }

    private void DetectHandPlayed(List<Card> cards)
    {
        handCards = new List<Card>(cards);
        realCards = new List<Card>(cards);
        if (handCards.Count == 0)
        {
            RemoveHandFromMult();
            cardsSorted.Clear();
            return;
        }
        if(CheckIfColor() && CheckIfFiveOfAKind())
        {
                Debug.Log("Flush Five");
                CardPlayer.instance.ReceiveHandCards(handCards);
                currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Flush_Five);
                AddHandToMult();
                return;            
        }
        if (CheckIfColor() && CheckIfFullHouse())
            {
                Debug.Log("Flush House");
                CardPlayer.instance.ReceiveHandCards(handCards);
                currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Flush_House);
                AddHandToMult();
                return;
            }
        if (CheckIfFiveOfAKind())
        {
            Debug.Log("Five of a kind");
            CardPlayer.instance.ReceiveHandCards(handCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Five_Of_A_Kind);
            AddHandToMult();
            return;
        }
        if (CheckIfColor() && CheckIfStraight())
        {
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Straight_Flush);
            CardPlayer.instance.ReceiveHandCards(handCards);
            Debug.Log("Straight Flush");
            AddHandToMult();
            return;
        }
        if (CheckIfColor())
        {
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Flush);
            CardPlayer.instance.ReceiveHandCards(handCards);
            Debug.Log("Suit");
            AddHandToMult();
            return;
        }

        if (CheckIfStraight())
        {
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Straight);
            Debug.Log("Straight");
            CardPlayer.instance.ReceiveHandCards(cardsSorted);
            AddHandToMult();
            return;
        }

        if (CheckIfFourOfAkind())
        {
            Debug.Log("Four of a Kind");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Four_Of_A_Kind);
            AddHandToMult();
            return;
        }

        if (CheckIfFullHouse())
        {
            Debug.Log("Full House");
            CardPlayer.instance.ReceiveHandCards(handCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Full_House);
            AddHandToMult();
            return;
        }

        if (CheckIfDoublePair())
        {
            Debug.Log("Double pair");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Double_Pair);
            AddHandToMult();
            return;
        }

        if (CheckIfThreeOfAKind())
        {
            CardPlayer.instance.ReceiveHandCards(realCards);
            Debug.Log("Three of a kind");
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Three_Of_A_Kind);
            AddHandToMult();
            return;
        }

        if (CheckIfPair())
        {

            Debug.Log("Pair");
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.Pair);
            AddHandToMult();
            return;
        }

        SetHighCard();
    }

    private void AddHandToMult()
    {
        handNameText.text = currentHand.pokerHand.name + " <color=blue> lvl." + currentHand.handLevel + "</color>";
        GameStatusManager.SetHandPlayed(currentHand.pokerHand.handType);
        CardPlayer.instance.SetHandPlayed(currentHand.pokerHand.handType);
        ScoreManager.instance.SetChips(currentHand.GetTotalChips());
        ScoreManager.instance.SetMult(currentHand.GetTotalMult());
    }

    public void RemoveHandFromMult()
    {
        handNameText.text = "";
        ScoreManager.instance.ResetChipsAndMult();
    }

    public bool CheckIfStraight()
    {
        if (handCards.Count < requiredAmmountForFlush)
        {
            cardsSorted.Clear();
            return false;
        }
        if (handCards.Find(x => x.cardType == CardType.Stone) != null) return false;
        cardsSorted = new List<Card>(handCards);
        cardsSorted = cardsSorted.OrderBy(x => x.number).ToList();
        int coincidences = 0;
        int diference;
        Card nextCard;

        if (cardsSorted.Any(x => x.number == 1))
        {
            cardsSorted.Add(new Card() { number = 14 }); coincidences++;
        }
        for (int i = 0; i < cardsSorted.Count; i++)
        {
            nextCard = i + 1 < cardsSorted.Count ? cardsSorted[i + 1] : null;
            if (nextCard != null)
            {
                diference = Mathf.Abs(cardsSorted[i].number - nextCard.number);
                if (diference > gapForStraights || diference == 0)
                {
                    coincidences--;
                }
                else
                {
                    coincidences++;
                }

            }
        }

        if (coincidences >= requiredAmmountForFlush - 1 && cardsSorted.Any(x => x.number == 14))
        {
            cardsSorted.RemoveAt(cardsSorted.Count - 1);
        }

        return coincidences >= requiredAmmountForFlush - 1;
    }
    public bool CheckIfColor()
    {
        if (handCards.Count < requiredAmmountForFlush)
        {
            return false;
        }
        if (handCards.Find(x => x.cardType == CardType.Stone) != null) return false;

        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards.FindAll(x => x.cardSuit == handCards[i].cardSuit || x.cardType == CardType.Wild).Count >= requiredAmmountForFlush)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckIfFiveOfAKind()
    {
        if (handCards.Count < 5)
        {
            return false;
        }

        if (handCards.Find(x => x.cardType == CardType.Stone) != null) return false;

        if (GetCardsInHandByNumber(handCards, handCards[0].number).Count == 5)
        {
            return true;
        }

        return false;
    }
    public bool CheckIfFourOfAkind()
    {
        if (handCards.Count < 4)
        {
            return false;
        }

        realCards = GetCardsInHandByNumber(handCards, handCards[0].number);
        if (realCards.Count == 4)
        {
            realCards.AddRange(handCards.FindAll(x => x.cardType == CardType.Stone));
            return true;
        }

        realCards = GetCardsInHandByNumber(handCards, handCards[3].number);
        if (realCards.Count == 4)
        {
            realCards.AddRange(handCards.FindAll(x => x.cardType == CardType.Stone));
            return true;
        }
        return false;
    }
    public bool CheckIfThreeOfAKind()
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
                realCards.AddRange(handCards.FindAll(x => x.cardType == CardType.Stone));
                return true;
            }
        }
        return false;
    }
    public bool CheckIfDoublePair()
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
                realCards.AddRange(handCards.FindAll(x => x.cardType == CardType.Stone));
            }
        }
        return pairsFound == 4;
    }
    public bool CheckIfFullHouse()
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
        return matches == 2;
    }
    public bool CheckIfPair()
    {
        if (handCards.Count < 2)
        {
            return false;
        }

        int matches = 0;

        realCards = new List<Card>();
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handCards[i].cardType == CardType.Stone)
            {
                realCards.Add(handCards[i]);
                continue;
            }
            if (GetCardsInHandByNumber(handCards, handCards[i].number).Count == 2)
            {
                realCards.Add(handCards[i]);
                matches++;
            }
        }
        return matches == 2;
    }
    public void SetHighCard()
    {
        if (handCards.Count > 0)
        {
            realCards = handCards.OrderBy(x => x.number).ToList();
            Card highestCard = realCards[realCards.Count - 1];

            realCards.Clear();

            realCards.Add(highestCard);
            realCards.AddRange(handCards.FindAll(x => x.cardType == CardType.Stone));
            CardPlayer.instance.ReceiveHandCards(realCards);
            currentHand = PokerHandLevelStorage.instance.GetHandData(HandType.High_Card);
            AddHandToMult();
        }
    }
    private List<Card> GetCardsInHandByNumber(List<Card> cards, int predicate)
    {
        List<Card> validCards = cards.FindAll(x => x.number == predicate && x.cardType != CardType.Stone);
        return validCards;
    }

    private void ResetValues()
    {
        realCards.Clear();
        handCards.Clear();
        cardsSorted.Clear();
    }
}
