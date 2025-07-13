using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public static CardPlayer instance;
    public List<Card> currentHand;
    private HandType lastHandPlayed;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameEventsManager.instance.OnHandPlayed += () => StartCoroutine(nameof(PlayCards));
    }
    public void ReceiveHandCards(List<Card> cards)
    {
        currentHand = cards;

        currentHand = currentHand.OrderBy(x => x.linkedCard.transform.GetSiblingIndex()).ToList();
        GameStatusManager.SetHandSize(currentHand.Count);
    }
    public void SetHandPlayed(HandType handData)
    {
        lastHandPlayed = handData;
    }

    private IEnumerator PlayCards()
    {

        for (int i = 0; i < currentHand.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            ScoreManager.instance.AddChips(currentHand[i].number);
            ScoreSign.instance.SetScoreSign(currentHand[i]);
            currentHand[i].linkedCard.pointerInteraction.ShakeCard();
            GameEventsManager.instance.TriggerCardPlayed(currentHand[i]);

            GameStatusManager.SetLastCardPlayed(currentHand[i]);
        }

        currentHand.Clear();
        yield return new WaitForSeconds(0.5f);

        GameEventsManager.instance.TriggerSpecificandPlayed(lastHandPlayed);

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Hand end");
        GameEventsManager.instance.TriggerHandEnd();
        GameStatusManager.SetGameEvent(TriggerOptions.HandEnd);

        yield return new WaitForSeconds(0.5f);
        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.3f);
        StartCoroutine(HandManager.instance.ClearHandPlayed());

        yield return new WaitForSeconds(0.3f);
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();

    }
}
