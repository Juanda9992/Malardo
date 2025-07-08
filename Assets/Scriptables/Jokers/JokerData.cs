using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;
    public string triggerMessage;

    public TriggerData[] triggerEvents;
    public GiveEvent giveEvent;
}

[System.Serializable]
public class TriggerData
{
    public TriggerOptions triggerOption;
    public string extraData;
}

public enum TriggerOptions
{
    OnHandPlay, OnHandEnd, OnCardPlay, OnHandDiscard
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