using System.Collections.Generic;
using UnityEngine;

public class HandDetector : MonoBehaviour
{

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
}
