using UnityEngine;
using UnityEngine.UI;

public static class CommonOperations
{
    private static Suit[] suits = new Suit[] { Suit.Clover, Suit.Diamond, Suit.Spades, Suit.Hearth };
    private static Seal[] seals = new Seal[] { Seal.None, Seal.Gold, Seal.Red, Seal.Blue, Seal.Purple };
    private static CardEdition[] cardEditions = new CardEdition[] { CardEdition.Base, CardEdition.Foil, CardEdition.Holographic, CardEdition.Polychrome };
    private static CardType[] cardTypes = new CardType[] { CardType.Gold, CardType.Stone, CardType.Silver, CardType.Lucky, CardType.Glass, CardType.Bonus, CardType.Mult, CardType.Wild };

    public static Suit GetRandomSuit()
    {
        return suits[Random.Range(0, suits.Length)];
    }

    public static CardType GetRandomCardType()
    {
        int generator = Random.Range(0, 100);

        if (generator < 30)
        {
            return cardTypes[Random.Range(0, cardTypes.Length)];
        }
        else
        {
            return CardType.Default;
        }
    }

    public static Seal CalculateRandomSeal()
    {
        if (Random.Range(0, 100) < 20)
        {
            return seals[Random.Range(1, seals.Length)];
        }
        else
        {
            return Seal.None;
        }
    }

    public static Seal GetRandomSeal(bool noneIncluded)
    {
        if (noneIncluded)
        {
            return seals[Random.Range(0, seals.Length)];
        }
        else
        {
            return seals[Random.Range(1, seals.Length)];
        }
    }

    public static CardEdition GenerateRandomCardEdition()
    {
        int chanceGenerator = Random.Range(0, 100);

        if (chanceGenerator < 92)
        {
            return CardEdition.Base;
        }
        else if (chanceGenerator < 96)
        {
            return CardEdition.Foil;
        }
        else if (chanceGenerator < 98)
        {
            return CardEdition.Holographic;
        }
        else
        {
            return CardEdition.Polychrome;
        }

    }
    public static CardEdition GetRandomCardEdition(bool noneIncluded)
    {
        if (noneIncluded)
        {
            return cardEditions[Random.Range(0, cardEditions.Length)];
        }
        else
        {
            return cardEditions[Random.Range(1, cardEditions.Length)];
        }
    }

    public static void DestroyChildsInParent(Transform parent)
    {
        Transform[] existingUI = parent.GetComponentsInChildren<Transform>();
        if (existingUI.Length > 1)
        {
            for (int i = 1; i < existingUI.Length; i++)
            {
                GameObject.Destroy(existingUI[i].gameObject);
            }
        }
    }

    public static DescriptionType GetJokerDescription(JokerData jokerData)
    {
        if (jokerData.jokerRarity == JokerRarity.Common)
        {
            return DescriptionType.Common;
        }
        else if (jokerData.jokerRarity == JokerRarity.Uncommon)
        {
            return DescriptionType.Uncommon;
        }
        else if (jokerData.jokerRarity == JokerRarity.Rare)
        {
            return DescriptionType.Rare;
        }
        else if (jokerData.jokerRarity == JokerRarity.Legendary)
        {
            return DescriptionType.Legendary;
        }
        return DescriptionType.None;
    }

    public static void UpdateCardSpacing(Transform parent, HorizontalLayoutGroup horizontalLayoutGroup, int minObjectsRequired)
    {
        float totalWidth = 0;
        float childCount = 0;

        RectTransform rectTransform = parent as RectTransform;

        foreach (RectTransform child in parent)
        {
            if (child.gameObject.activeSelf)
            {
                totalWidth += child.sizeDelta.x;
                childCount++;
            }
        }

        if (childCount > minObjectsRequired)
        {
            float availableWidth = rectTransform.rect.width
                                   - horizontalLayoutGroup.padding.left
                                   - horizontalLayoutGroup.padding.right
                                   - totalWidth;

            float spacing = availableWidth / (childCount - 1);
            horizontalLayoutGroup.spacing = spacing;
        }
        else
        {
            horizontalLayoutGroup.spacing = 0f;
        }
    }
}
