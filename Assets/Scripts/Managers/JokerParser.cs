using Unity.Collections;
using UnityEngine;

public class JokerParser : MonoBehaviour
{
    public static JokerParser instance;

    void Awake()
    {
        instance = this;
    }
    public void ParseJoker(JokerContainer jokercontainer)
    {
        JokerData jokerData = jokercontainer._joker;
        jokercontainer.JokerAction += () => Debug.Log(jokercontainer._joker.jokerName);
        HandleTriggerEvents(jokercontainer);
        HandleExecutionEvents(jokercontainer);
    }
    private void HandleTriggerEvents(JokerContainer jokerContainer)
    {
        JokerData jokerData = jokerContainer._joker;
        for (int i = 0; i < jokerData.triggerEvents.Length; i++)
        {
            switch (jokerData.triggerEvents[i].triggerOption)
            {
                case TriggerOptions.OnHandPlay:
                    GameEventsManager.instance.OnHandPlayed += () => jokerContainer.JokerAction?.Invoke();
                    break;

                case TriggerOptions.OnHandEnd:
                    GameEventsManager.instance.OnHandEnd += () => jokerContainer.JokerAction?.Invoke();
                    break;

                case TriggerOptions.OnHandDiscard:
                    GameEventsManager.instance.OnHandDiscarted += () => jokerContainer.JokerAction?.Invoke();
                    break;
            }

        }
        if (jokerData.requiredCardPlayedData.active)
        {
            GameEventsManager.instance.OnCardPlay += x =>
            {

                Debug.Log(jokerData.requiredCardPlayedData.cardSuit);
                if (x.cardSuit == jokerData.requiredCardPlayedData.cardSuit)
                {
                    if (jokerData.requiredCardPlayedData.number > 0)
                    {
                        if (x.number == jokerData.requiredCardPlayedData.number)
                        {
                            jokerContainer.JokerAction?.Invoke();
                        }
                    }
                    else
                    {
                        jokerContainer.JokerAction?.Invoke();
                    }
                }
            };
        }

    }

    private void HandleExecutionEvents(JokerContainer jokerContainer)
    {
        JokerData jokerData = jokerContainer._joker;

        if (jokerData.giveEvent.active)
        {
            jokerContainer.JokerAction += () => jokerData.giveEvent.GiveAction();
        }

    }
}
