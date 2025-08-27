using UnityEngine;

[CreateAssetMenu(fileName = "Hand Data", menuName = "Scriptables/Hands/Hand Data")]
public class HandData : ScriptableObject
{
    public int baseChips;
    public int baseMult;
    public HandType handType;
    public int chipsUpgrade;
    public int multUpgrade;
}

public enum HandType
{
    High_Card = 0,
    Pair = 1,
    Double_Pair = 2,
    Three_Of_A_Kind = 3,
    Straight = 4,
    Flush = 5,
    Full_House = 6,
    Four_Of_A_Kind = 7,
    Five_Of_A_Kind = 8,
    Straight_Flush = 9,
    Flush_House = 10
}
