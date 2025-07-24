using UnityEngine;

[CreateAssetMenu(fileName = "BlindScoreData", menuName = "Scriptables/Blind/Blind Score Data")]
public class BlindScoreData : ScriptableObject
{
    public int[] baseScore;
    public BlindData[] allBlinds;
}

[System.Serializable]
public class BlindData
{
    public string blindName;
    public float scoreMultiplier;
    public int blindMoney;

    public Color blindColor;
}
