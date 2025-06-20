using UnityEngine;

[CreateAssetMenu(fileName = "Hand Data", menuName = "Scriptables/Hands/Hand Data")]
public class HandData : ScriptableObject
{
    public int baseChips;
    public int baseMult;
    public HandType handType;
}

public enum HandType
{
    High_Card,
    Pair,
    Double_Pair,
    Three_Of_A_Kind,
    Straight,
    Flush,
    Full_House
}
