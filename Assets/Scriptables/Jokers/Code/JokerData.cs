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
    public List<JokerTrigger> triggers;
    public List<JokerEffect> effects;

    public List<JokerEffect> OnSetUpJoker;
    public List<JokerEffect> OnSellEffect;
    public JokerEffect reactivationJoker;
    public JokerInstance jokerInstance;
}

[System.Serializable]
public class JokerInstance
{
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
    }

    public int sellValue { get { return Mathf.FloorToInt((float)data.shopValue / 2); } }
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

