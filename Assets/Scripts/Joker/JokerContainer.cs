using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerContainer : MonoBehaviour
{
    public JokerInstance _jokerInstance;
    public bool isOnShop = true;

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
        _jokerInstance.SetJokerContainer(this);
    }

    private void HandleTriggerEvents(GameStatus gameStatus)
    {
        if (_jokerInstance.data.triggers.Count == 0)
        {
            return;
        }

        foreach (var logics in _jokerInstance.data.jokerLogics)
        {
            for (int i = 0; i < logics.jokerTrigger.Length; i++)
            {
                if (!_jokerInstance.data.triggers[i].MeetCondition(gameStatus))
                {
                    break; ;
                }
                TriggerActions(logics);
            }

        }

    }

    public bool CanBeTriggered()
    {

        foreach (var logics in _jokerInstance.data.jokerLogics)
        {
            for (int i = 0; i < logics.jokerTrigger.Length; i++)
            {
                if (!logics.jokerTrigger[i].MeetCondition(GameStatusManager._Status))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void TriggerMessage()
    {
        ScoreSign.instance.SetMessage(Color.green, _jokerInstance.triggerMessage, transform.position);
    }

    public void TriggerActions(JokerLogic logics)
    {
        JokerExecuteAction(logics);
    }

    private void JokerExecuteAction(JokerLogic jokerLogic)
    {
        for (int i = 0; i < jokerLogic.jokerEffect.Length; i++)
        {
            jokerLogic.jokerEffect[i].ApplyEffect(_jokerInstance);
            jokerLogic.jokerEffect[i].ammount = _jokerInstance.data.overrideEffect;

            if (jokerLogic.jokerEffect[i].jokerOutput != string.Empty)
            {
                if (jokerLogic.jokerEffect[i].jokerOutput == "Destroy")
                {
                    Destroy(gameObject);
                }
            }
        }
        ScoreSign.instance.SetJokerSign(_jokerInstance.triggerMessage, transform.position);
    }
}
