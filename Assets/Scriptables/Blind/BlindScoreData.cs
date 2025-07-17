using UnityEngine;

[CreateAssetMenu(fileName = "BlindScoreData", menuName = "Scriptables/Blind/Blind Score Data")]
public class BlindScoreData : ScriptableObject
{
    public int[] baseScore;
    public float[] scoreMultiplier;
    public int[] blindMoney;
}
