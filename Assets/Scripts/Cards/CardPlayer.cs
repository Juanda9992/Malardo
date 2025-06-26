using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class CardPlayer : MonoBehaviour
{
    private List<Card> currentHand;
    void Start()
    {
        HandManager.instance.OnHandPlayed += ReceiveHandCards;
    }
    public void ReceiveHandCards(List<Card> cards)
    {
        currentHand = cards;
        StartCoroutine(nameof(PlayCards));
    }

    private IEnumerator PlayCards()
    {
        for (int i = 0; i < currentHand.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            ScoreManager.instance.AddChips(currentHand[i].number);
            GameEventsManager.instance.TriggerCardPlayed(currentHand[i]);
        }

        yield return new WaitForSeconds(0.5f);

        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.3f);

        foreach (var card in currentHand)
        {
            card.linkedCard.pointerInteraction.DiscardCard();
        }

        yield return new WaitForSeconds(0.3f);

        currentHand.Clear();
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();

    }
}
