
using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Modify Stats Blind", menuName = "Scriptables/Blind/Modify Stats Blind")]
public class ModifyStatsBlind : CurrentBlind
{
    public bool oneHandSizeLess;
    public bool noDiscards;
    public int handAmmount = -1;
    public int _cardsToPlay = -1;

    public bool _loseMoneyPerCard;
    public bool _decreasePokerLevel;
    public bool divideChipsAndMult;
    public static bool decreasePokerLevel;
    public static int cardsToPlay = -1;
    public static bool divideScore = false;
    public static bool loseMoneyPerCard;
    public bool _banMostPlayedHand;
    public static bool banMostPlayedHand;
    public bool _discardCards;
    public static bool discardCards;

    private int lastDiscards;
    private int lastHands;
    [ContextMenu("Test Effect")]
    public override void ApplyEffect()
    {
        if (noDiscards)
        {
            lastDiscards = HandManager.instance.defaultDiscards;
            HandManager.instance.SetDefaultDiscards(0);
        }
        if (handAmmount > 0)
        {
            lastHands = HandManager.instance.GetHandsRemaining();
            HandManager.instance.SetDefaultHands(handAmmount, true);
        }
        if (oneHandSizeLess)
        {
            DeckManager.instance.AddHandSize(-1);
        }

        if (_cardsToPlay > 0)
        {
            cardsToPlay = _cardsToPlay;
        }

        if (_decreasePokerLevel)
        {
            decreasePokerLevel = true;
        }

        if (divideChipsAndMult)
        {
            divideScore = true;
        }

        if (_loseMoneyPerCard)
        {
            loseMoneyPerCard = true;
        }

        if (_banMostPlayedHand)
        {
            banMostPlayedHand = true;
        }

        if (_discardCards)
        {
            discardCards = true;
        }
        BlindManager.instance.SetCustomRequiredScore((int)(BlindManager.instance.GetRoundBaseScore() * blindMultiplier));

    }
    [ContextMenu("Revert Effect")]
    public override void RevertEffect()
    {
        if (noDiscards)
        {
            HandManager.instance.SetDefaultDiscards(lastDiscards, true);
        }

        if (handAmmount > 0)
        {
            HandManager.instance.SetDefaultHands(lastHands, true);
        }

        if (oneHandSizeLess)
        {
            DeckManager.instance.AddHandSize(+1);
        }
        if (_cardsToPlay > 0)
        {
            cardsToPlay = -1;
        }
        if (_decreasePokerLevel)
        {
            decreasePokerLevel = false;
        }

        if (divideChipsAndMult)
        {
            divideScore = false;
        }

        if (_loseMoneyPerCard)
        {
            loseMoneyPerCard = false;
        }

        if (_banMostPlayedHand)
        {
            banMostPlayedHand = false;
        }

        if (_discardCards)
        {
            discardCards = false;
        }
        BlindManager.instance.SetCustomRequiredScore(BlindManager.instance.GetRoundBaseScore() * 2);
    }

    public override IEnumerator CheckEffect()
    {
        if (cardsToPlay > 0)
        {
            if (HandManager.instance.handCards.Count < cardsToPlay)
            {
                InvalidateHand();
                yield return new WaitForSeconds(2f);

            }
        }
        if (decreasePokerLevel)
        {
            PokerHandUpgrader.instance.RequestDecreasePokerHand(HandDetector.instance.currentHand.pokerHand.handType);
        }

        if (divideScore)
        {
            ScoreManager.instance.DivideChipsAndMult();
            yield return new WaitForSeconds(0.5f);
        }
        if (loseMoneyPerCard)
        {
            Card card_Data;
            for (int i = 0; i < HandManager.instance.handCards.Count; i++)
            {
                card_Data = HandManager.instance.handCards[i];
                ScoreSign.instance.SetMessage(Color.yellow, "-$1", card_Data.linkedCard.transform.position);
                card_Data.linkedCard.pointerInteraction.ShakeCard();
                CurrencyManager.instance.RemoveCurrency(1);

                yield return new WaitForSeconds(0.2f);
            }
        }
        if (banMostPlayedHand)
        {
            if (CommonOperations.CheckIfMostPlayedHand())
            {
                CurrencyManager.instance.SetCurrency(0);
                yield return new WaitForSeconds(0.3f);
            }
        }

        if (discardCards)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 2; i++)
            {
                if (CardManager.instance.cardsOnScreen.Count > 0)
                {
                    Card_Data card_Data = CardManager.instance.cardsOnScreen[Random.Range(0, CardManager.instance.cardsOnScreen.Count)];

                    CardManager.instance.cardsOnScreen.Remove(card_Data);
                    card_Data.pointerInteraction.RemoveCard();
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void InvalidateHand()
    {
        BlindManager.instance.ShowInvalidateMessage();
        CardPlayer.instance.StopPlayCoroutine();
    }
}
