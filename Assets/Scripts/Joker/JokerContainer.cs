using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerContainer : MonoBehaviour
{
    public JokerInstance _jokerInstance;
    public bool isOnShop = true;

    [SerializeField] private TextMeshProUGUI jokerText;

    [SerializeField] private Image jokerBg;
    [SerializeField] private GameObject[] editionsContainer;
    private bool triggered;
    public void SetUpJoker(JokerInstance jokerData)
    {
        _jokerInstance = jokerData;
        jokerText.text = _jokerInstance.data.jokerName;

        if (jokerData.data.JokerImage != null)
        {
            jokerText.gameObject.SetActive(false);
            jokerBg.sprite = jokerData.data.JokerImage;
        }
        _jokerInstance.SetJokerContainer(this);

        foreach (var logic in _jokerInstance.jokerLogics)
        {
            foreach (var effect in logic.jokerEffect)
            {
                effect.SetupEffect(_jokerInstance);
                effect.UpdateDescription(_jokerInstance);
            }
        }

        TrySetEdition();
        StartCoroutine("UpdateJokerDesc");
    }

    private void TrySetEdition()
    {
        SetJokerEdition(_jokerInstance.jokerEdition);
    }

    public void DropJoker()
    {
        JokerManager.instance.ReorderJokers();
    }

    public void SetJokerEdition(CardEdition cardEdition)
    {
        _jokerInstance.jokerEdition = cardEdition;

        if ((int)cardEdition >= 0)
        {
            editionsContainer[(int)cardEdition].SetActive(true);
        }

        if (_jokerInstance.jokerEdition == CardEdition.Negative)
        {
            Debug.LogError("Stop Here");
        }
    }

    public void ShowDescription()
    {
        GetComponent<DescriptionContainer>().SetNameAndDescription(_jokerInstance.data.jokerName, _jokerInstance.jokerDescription, CommonOperations.GetJokerDescription(_jokerInstance.data), _jokerInstance.jokerEdition);
        GetComponent<DescriptionContainer>().ShowDescription();
    }

    public void TriggerMessage()
    {
        triggered = true;
        ScoreSign.instance.SetMessage(Color.green, _jokerInstance.triggerMessage, transform.position);
    }

    public void TriggerMessage(string content)
    {
        triggered = true;
        ScoreSign.instance.SetMessage(Color.green, content, transform.position);
    }

    public void TriggerActions(JokerLogic logics)
    {
        JokerExecuteAction(logics);
        transform.DOPunchScale(Vector3.one * 0.3f, 0.15f);
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

    private IEnumerator UpdateJokerDesc()
    {
        while (true)
        {
            foreach (var logic in _jokerInstance.jokerLogics)
            {
                foreach (var effect in logic.jokerEffect)
                {
                    effect.UpdateDescription(_jokerInstance);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
