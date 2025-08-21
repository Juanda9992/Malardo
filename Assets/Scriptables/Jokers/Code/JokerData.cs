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
    public int sellValue { get { return Mathf.FloorToInt((float)shopValue / 2); } }
    public JokerRarity jokerRarity;
    public List<JokerTrigger> triggers;
    public List<JokerEffect> effects;

    public List<JokerEffect> OnSetUpJoker;
    public List<JokerEffect> OnSellEffect;
    public JokerEffect reactivationJoker;

    public JokerVisuals jokerVisuals;
    public bool requireInstance = false;
    public JokerInstance jokerInstance = new JokerInstance();


    [ContextMenu("Set Data")]
    private void SetNameAndDescription()
    {
        jokerInstance.jokerDescription = description;
        jokerInstance.triggerMessage = triggerMessage;
    }
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

[System.Serializable]
public class JokerVisuals
{
    public Color bgColor;
}
