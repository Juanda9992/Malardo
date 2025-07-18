using System;
using TMPro;
using UnityEngine;

public class JokerContainer : MonoBehaviour
{
    public JokerData _joker;
    public bool isOnShop = true;
    public Action JokerAction;

    [SerializeField] private TextMeshProUGUI jokerText;

    void Start()
    {
        GameStatusManager.OnStatusChanged += HandleTriggerEvents;
    }

    public void SetUpJoker(JokerData jokerData)
    {
        _joker = jokerData;
        jokerText.text = _joker.jokerName;

        JokerAction += JokerExecuteAction;
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

    private void JokerExecuteAction()
    {
        for (int i = 0; i < _joker.effects.Count; i++)
        {
            _joker.effects[i].ammount = _joker.overrideEffect;
            _joker.triggerMessage = _joker.effects[i].GetCustomMessage() == string.Empty ? _joker.triggerMessage : _joker.effects[i].GetCustomMessage();
            _joker.effects[i].ApplyEffect();

            if (_joker.effects[i].jokerOutput != string.Empty)
            {
                if (_joker.effects[i].jokerOutput == "Destroy")
                {
                    Destroy(gameObject);
                }
            }
        }
        ScoreSign.instance.SetJokerSign(_joker.triggerMessage, transform.position);
    }


    void OnDisable()
    {
        JokerAction -= JokerExecuteAction;
    }

    [ContextMenu("Test Joker")]
    private void TestJoker()
    {
        JokerAction?.Invoke();
    }
}
