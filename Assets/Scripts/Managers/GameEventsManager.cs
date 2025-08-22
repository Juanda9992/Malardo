using System;
using System.Collections;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    [SerializeField] private bool debugger;
    public event Action OnHandPlayed;
    public event Action OnHandEnd;
    public event Action OnHandDiscarted;
    public event Action<HandType> OnSpecificHandPlayed;
    public event Action<Card> OnCardPlay;
    public event Action OnRoundBegins;
    public event Action OnRoundEnds;

    void Awake()
    {
        instance = this;
    }

    public void TriggerHandPlayed()
    {
        OnHandPlayed?.Invoke();

        if (debugger) Debug.Log("Hand Played");
    }

    public void TriggerSpecificandPlayed(HandType handData)
    {
        OnSpecificHandPlayed?.Invoke(handData);

        if (debugger) Debug.Log(handData + " " + "Played");
    }

    public void TriggerHandEnd()
    {
        OnHandEnd?.Invoke();

        if (debugger) Debug.Log("Hand Ended");
    }

    public void TriggerHandDiscard()
    {
        OnHandDiscarted?.Invoke();

        if (debugger) Debug.Log("Hand Discard");
    }

    public void TriggerCardPlayed(Card card)
    {
        OnCardPlay?.Invoke(card);

        if (debugger) card.DegubCardInfo();
    }

    public void TriggerRoundBegins()
    {
        GameStatusManager.SetGameEvent(TriggerOptions.RoundBegin);
        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.RoundBegin));
        OnRoundBegins?.Invoke();
    }

    public void TriggerRoundEnd()
    {
        OnRoundEnds?.Invoke();
    }
}
