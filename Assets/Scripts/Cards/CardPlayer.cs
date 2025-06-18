using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CardPlayer : MonoBehaviour
{

    void Start()
    {
        HandManager.instance.OnHandPlayed += ReceiveHandCards;
    }
    public void ReceiveHandCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.DegubCardInfo();
        }
    }
}
