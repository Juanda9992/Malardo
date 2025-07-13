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

        JokerAction += () =>
        {
            Debug.Log(_joker.effects.Count);
            for (int i = 0; i < _joker.effects.Count; i++)
            {
                _joker.effects[i].ammount = _joker.overrideEffect;
                _joker.triggerMessage = _joker.effects[i].GetCustomMessage() == string.Empty ? _joker.triggerMessage : _joker.effects[i].GetCustomMessage(); 
                _joker.effects[i].ApplyEffect();
            }
            ScoreSign.instance.SetJokerSign(_joker.triggerMessage, transform.position);
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
