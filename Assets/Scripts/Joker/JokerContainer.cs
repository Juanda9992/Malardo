using System;
using TMPro;
using UnityEngine;

public class JokerContainer : MonoBehaviour
{
    public JokerData _joker;

    public Action JokerAction;

    [SerializeField] private TextMeshProUGUI jokerText; 

    void Start()
    {
        Debug.Log(_joker.jokerName);
        GameStatusManager.OnStatusChanged += HandleTriggerEvents;
        SetUpJoker();
    }

    public void SetUpJoker()
    {
        jokerText.text = _joker.jokerName;

        JokerAction += () => ScoreSign.instance.SetJokerSign(_joker.triggerMessage, transform.position);
    }

    public void ParseJoker()
    {
        JokerAction += () =>
        {
            for (int i = 0; i < _joker.effects.Count; i++)
            {
                _joker.effects[i].ammount = _joker.overrideEffect;
                _joker.effects[i].ApplyEffect();
            }
        };
    }
    private void HandleTriggerEvents(GameStatus gameStatus)
    {
        for (int i = 0; i < _joker.triggers.Count; i++)
        {
            if (!_joker.triggers[i].MeetCondition(gameStatus))
            {
                return;
            }
        }

        JokerAction?.Invoke();

    }

    [ContextMenu("Test Joker")]
    private void TestJoker()
    {
        JokerAction?.Invoke();
    }
}
