using System.Collections.Generic;
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

    [Header("Description Colors")]
    public List<DescriptionColor> descriptionColors;
    [Header("Suit Colors")]
    public SuitColorData[] suitColors;

    [Header("Card Edition Colors")]
    public Color foilColor;
    public Color holographicColor;

    public Color defaultBgColor;
    public Color buffonPackBgColor;
    public Color cardPackBgColor;
    public Color planetPackBgColor;


    public Color GetSuitColor(Suit suit)
    {
        for (int i = 0; i < suitColors.Length; i++)
        {
            if (suit == suitColors[i].suitId)
            {
                if (SettingsManager.instance.useAltColors)
                {
                    return suitColors[i].altColor;
                }
                else
                {
                    return suitColors[i].normalColor;
                }
            }
        }
        return Color.white;
    }
}
[System.Serializable]
public class DescriptionColor
{
    public DescriptionType descriptionType;
    public Color instanceColor;
    public static DescriptionType GetTypeByjokerRarity(JokerInstance jokerInstance)
    {
        if (jokerInstance.data.jokerRarity == JokerRarity.Common)
        {
            return DescriptionType.Common;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Uncommon)
        {
            return DescriptionType.Uncommon;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Rare)
        {
            return DescriptionType.Rare;
        }
        else if (jokerInstance.data.jokerRarity == JokerRarity.Legendary)
        {
            return DescriptionType.Legendary;
        }

        return DescriptionType.None;
    }
}
[System.Serializable]
public class SuitColorData
{
    public Suit suitId;
    public Color normalColor;
    public Color altColor;
}