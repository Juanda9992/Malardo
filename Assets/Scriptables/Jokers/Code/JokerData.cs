using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker/Template")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;
    public string triggerMessage;
    public float overrideEffect = 0;
    public int shopValue;
    public JokerRarity jokerRarity;
    public JokerLogic[] jokerLogics;
    public List<JokerEffect> OnSetUpJoker;
    public List<JokerEffect> OnSellEffect;
    public JokerEffect reactivationJoker;
}

[System.Serializable]
public class JokerInstance
{
    public JokerContainer jokerContainer;
    public string jokerDescription;
    public string triggerMessage;
    public JokerData data;

    public int totalChips;
    public int totalMult;
    public int totalMultiplier;
    public JokerInstance(JokerData jokerData)
    {
        data = jokerData;
        jokerDescription = data.description;
        triggerMessage = data.triggerMessage;
        jokerLogics = jokerData.jokerLogics;
    }

    public void SetJokerContainer(JokerContainer container)
    {
        jokerContainer = container;
    }

    public JokerLogic[] jokerLogics;
    public bool destroyJoker;

    public int sellValue { get { return Mathf.FloorToInt((float)data.shopValue / 2); } }
}

[System.Serializable]
public class JokerLogic
{
    public TriggerEvent triggerEvent;
    public bool CanBetriggered()
    {
        for (int i = 0; i < jokerTrigger.Length; i++)
        {
            if (!jokerTrigger[i].MeetCondition(GameStatusManager._Status))
            {
                return false;
            }
        }
        return true;
    }
    public JokerTrigger[] jokerTrigger;
    public JokerEffect[] jokerEffect;
}

public enum TriggerEvent
{
    OnHandPlay,
    OnHandEnd,
    OnCardPlay
}
public enum JokerRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[System.Serializable]
public class RequiredHandSizeData
{
    public bool active;
    public int minAmmount;
    public int maxAmmount;
}

