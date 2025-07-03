using UnityEngine;

[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;

    public TriggerData[] triggerEvents;
    public ExecuteEvents[] executeEvents;
}

[System.Serializable]
public class TriggerData
{
    public TriggerOptions triggerOption;
    public string extraData;
}

[System.Serializable]
public class ExecuteEvents
{
    public ExecuteAction executeAction;
    public string executeActionExtraData;
}
public enum ExecuteAction
{
    AddMult, AddChips, MultiplyMult
}

public enum TriggerOptions
{
    OnHandPlay,OnHandEnd, OnCardPlay, OnHandDiscard
}