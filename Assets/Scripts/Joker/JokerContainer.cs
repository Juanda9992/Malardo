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
    private bool triggered;
    public void SetUpJoker(JokerInstance jokerData)
    {
        _jokerInstance = jokerData;
        jokerText.text = _jokerInstance.data.jokerName;
        _jokerInstance.SetJokerContainer(this);

        foreach (var logic in _jokerInstance.jokerLogics)
        {
            foreach (var effect in logic.jokerEffect)
            {
                effect.SetupEffect(_jokerInstance);
                effect.UpdateDescription(_jokerInstance);
            }
        }
    }

    public void TriggerMessage()
    {
        triggered = true;
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
            jokerLogic.jokerEffect[i].ammount = _jokerInstance.data.overrideEffect;
            jokerLogic.jokerEffect[i].ApplyEffect(_jokerInstance);
        }

        if (_jokerInstance.triggerMessage != String.Empty && !triggered)
        {
            ScoreSign.instance.SetJokerSign(_jokerInstance.triggerMessage, transform.position);
        }
    }
}
