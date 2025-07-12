using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;
    public string triggerMessage;
    public int overrideEffect = 0;
    public JokerRarity jokerRarity;
    public DefaultTriggerEvent defaultTrigger;
    public RequiredCardPlayedData requiredCardPlayedData;
    public RequiredHandPlayed requiredHandPlayed;
    public RequiredHandSizeData requiredHandSizeData;

    public List<JokerEffect> effects;
}
public enum JokerRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[System.Serializable]
public class DefaultTriggerEvent
{
    public bool active;
    public TriggerOptions triggerOptions;
}

[System.Serializable]
public class RequiredCardPlayedData
{
    public bool active;
    public Suit cardSuit;
    public int number = -1;
}
[System.Serializable]
public class RequiredHandPlayed
{
    public bool active;
    public HandType requiredHand;
}

[System.Serializable]
public class RequiredHandSizeData
{
    public bool active;
    public int minAmmount;
    public int maxAmmount;
}

public enum TriggerOptions
{
    OnHandPlay, OnHandEnd, OnCardPlay, OnHandDiscard
}

[System.Serializable]
public class GiveModifier
{
    public bool active;
    public QuantityType forEach;
    public int give;
    public GiveEvent.GiveType giveType;
}
public enum QuantityType
{
    Hand, Discard
}

[System.Serializable]
public class GiveEvent
{
    public enum GiveType { Chips, Mult }
    public bool active;
    public int ammount;
    public GiveType giveType;
    public void GiveAction()
    {
        if (giveType == GiveType.Chips)
        {
            ScoreManager.instance.AddChips(ammount);
        }
        else if (giveType == GiveType.Mult)
        {
            ScoreManager.instance.AddMult(ammount);
        }
    }
}