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
        }

        yield return new WaitForSeconds(0.5f);

        GameEventsManager.instance.TriggerSpecificandPlayed(lastHandPlayed);
        Debug.Log(lastHandPlayed);

        yield return new WaitForSeconds(0.5f);

        GameEventsManager.instance.TriggerHandEnd();

        yield return new WaitForSeconds(1f);

        ScoreManager.instance.CalculateScore();

        yield return new WaitForSeconds(0.3f);

        HandManager.instance.DiscardAllCards();

        yield return new WaitForSeconds(0.35f);

        currentHand.Clear();
        HandDetector.instance.RemoveHandFromMult();
        ScoreManager.instance.ResetChipsAndMult();

    }
}
