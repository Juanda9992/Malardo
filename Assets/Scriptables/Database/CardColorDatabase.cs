using UnityEngine;

[CreateAssetMenu(fileName = "Color Database", menuName = "Scriptables/Database/Color Database")]
public class CardColorDatabase : ScriptableObject
{
    [Header("Card Type Colors")]
    public Color goldCard;
    public Color steelCard;
    public Color stoneCard;
    public Color luckyCard;
    public Color glassCard;

    [Header("Card Edition Colors")]
    public Color foilColor;
    public Color holographicColor;

    public Color defaultBgColor;
    public Color buffonPackBgColor;
    public Color cardPackBgColor;
    public Color planetPackBgColor;
}
