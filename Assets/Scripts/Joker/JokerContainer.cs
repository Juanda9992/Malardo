using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerContainer : MonoBehaviour
{
    public JokerInstance _jokerInstance;
    public bool isOnShop = true;
    public Action JokerAction;

    [SerializeField] private TextMeshProUGUI jokerText;

    [SerializeField] private Image jokerBg;
    void Start()
    {
        GameStatusManager.OnStatusChanged += HandleTriggerEvents;
    }

    public void SetUpJoker(JokerInstance jokerData)
    {
        _jokerInstance = jokerData;
        jokerText.text = _jokerInstance.data.jokerName;

        JokerAction += JokerExecuteAction;
    }

    private void HandleTriggerEvents(GameStatus gameStatus)
    {
        if (_jokerInstance.data.triggers.Count == 0)
        {
            return;
        }
        for (int i = 0; i < _jokerInstance.data.triggers.Count; i++)
        {
            if (!_jokerInstance.data.triggers[i].MeetCondition(gameStatus))
            {
                return;
            }
        }

        JokerAction?.Invoke();

    }

    public bool CanBeTriggered()
    {
        for (int i = 0; i < _jokerInstance.data.triggers.Count; i++)
        {
            if (!_jokerInstance.data.triggers[i].MeetCondition(GameStatusManager._Status))
            {
                return false;
            }
        }
        return true;
    }

    public void TriggerMessage()
    {
        ScoreSign.instance.SetMessage(Color.green, _jokerInstance.data.triggerMessage, transform.position);
    }

    public void TriggerActions()
    {
        JokerExecuteAction();
    }

    private void JokerExecuteAction()
    {
        for (int i = 0; i < _jokerInstance.data.effects.Count; i++)
        {
            _jokerInstance.data.effects[i].ammount = _jokerInstance.data.overrideEffect;
            _jokerInstance.data.triggerMessage = _jokerInstance.data.effects[i].GetCustomMessage() == string.Empty ? _jokerInstance.data.triggerMessage : _jokerInstance.data.effects[i].GetCustomMessage();
            _jokerInstance.data.effects[i].ApplyEffect();

            if (_jokerInstance.data.effects[i].jokerOutput != string.Empty)
            {
                if (_jokerInstance.data.effects[i].jokerOutput == "Destroy")
                {
                    Destroy(gameObject);
                }
            }
        }
        ScoreSign.instance.SetJokerSign(_jokerInstance.data.triggerMessage, transform.position);
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
