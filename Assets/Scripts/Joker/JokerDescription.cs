using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerDescription : MonoBehaviour
{
    public static JokerDescription instance;

    [SerializeField] private TextMeshProUGUI nameText, descriptionText;
    [SerializeField] private float cardDescriptionOffsetY;
    [SerializeField] private ExtraTagContainer[] extraTags;

    [SerializeField] private TextMeshProUGUI editionTitle, editionDescription;
    [SerializeField] private GameObject editionContainer;

    [SerializeField] private TextMeshProUGUI sealTitle, sealDescription;
    [SerializeField] private GameObject sealContainer;
    private RectTransform contentText;
    [SerializeField] private RectTransform contentParent;
    void Awake()
    {
        instance = this;
        SetDescriptionOff();
        contentText = descriptionText.GetComponent<RectTransform>();
    }
    public void SetDescriptionOff()
    {
        transform.localScale = Vector2.zero;
    }

    public void SetCardDescription(Card card,Transform cardTransform)
    {
        nameText.text = card.cardName;
        transform.localScale = Vector2.one;
        transform.position = (Vector2)cardTransform.transform.position + new Vector2(0, cardDescriptionOffsetY);
        descriptionText.text = $"<style=Chips>+{card.chipAmmount} </style>chips";
        SetCardData(card);
        StartCoroutine("ForceRebuildDesc");
    }

    private void SetCardData(Card card_Data)
    {
        if (card_Data.cardType == CardType.Default)
        {
            SetDescriptionRarity(extraTags[0], DescriptionType.None);
        }

        switch (card_Data.cardType)
        {
            case CardType.Glass:
                descriptionText.text += '\n' + "<style=Mult>X2 Mult</style> \n 1 in 4 chance to destroy card";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Glass Card");
                break;

            case CardType.Stone:
                descriptionText.text += '\n' + "no rank or suit";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Stone Card");
                break;
            case CardType.Lucky:
                descriptionText.text += '\n' + "1 in 5 chance \n for <style=Mult>+20 mult</style>\n1 in 15 chance \n to win <style=Cash>$20</style> ";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Lucky Card");
                break;
            case CardType.Silver:
                descriptionText.text += '\n' + "<style=Mult>X1.5 Mult</style> \n while this card \n stays in hand";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Silver Card");
                break;
            case CardType.Gold:
                descriptionText.text += '\n' + "<style=Cash>$3</style> if this \n card is held in hand \n at the end of the round";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Gold Card");
                break;
            case CardType.Bonus:
                descriptionText.text += '\n' + "<style=Chips>+30</style> Extra Chips";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Bonus Card");
                break;
            case CardType.Mult:
                descriptionText.text += '\n' + "<style=Mult>+4</style> Mult";
                SetDescriptionRarity(extraTags[0], DescriptionType.Special_Card, "Mult Card");
                break;
        }

        switch (card_Data.cardSeal)
        {
            case Seal.None:
                SetDescriptionRarity(extraTags[1], DescriptionType.None);
                break;
            case Seal.Red:
                SetDescriptionRarity(extraTags[1], DescriptionType.Voucher, "Red Seal");
                break;
            case Seal.Blue:
                SetDescriptionRarity(extraTags[1], DescriptionType.Common, "Blue Seal");
                break;
            case Seal.Gold:
                SetDescriptionRarity(extraTags[1], DescriptionType.Gold_Seal, "Gold Seal");
                break;
        }

        switch (card_Data.cardEdition)
        {
            case CardEdition.Foil:
                SetDescriptionRarity(extraTags[2], DescriptionType.Special_Card, "Foil");
                break;
            case CardEdition.Holographic:
                SetDescriptionRarity(extraTags[2], DescriptionType.Special_Card, "Holographic");
                break;
            case CardEdition.Polychrome:
                SetDescriptionRarity(extraTags[2], DescriptionType.Special_Card, "Polychrome");
                break;
        }

        SetEditionDescription(card_Data.cardEdition);
        SetSealDescription(card_Data.cardSeal);
    }

    public void SetGenericDescription(string itemName, string itemDescription, Vector2 itemPosition, DescriptionType descriptionType, CardEdition cardEdition = CardEdition.Base)
    {
        nameText.text = itemName;
        descriptionText.text = itemDescription;
        transform.position = itemPosition;
        transform.localScale = Vector2.one;

        SetDescriptionRarity(extraTags[0], descriptionType);
        SetEditionDescription(cardEdition);
        StartCoroutine("ForceRebuildDesc");


    }
    private IEnumerator ForceRebuildDesc()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentText);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentText);
    }

    private void SetDescriptionRarity(ExtraTagContainer extraTagContainer, DescriptionType descriptionType, string auxText = "")
    {
        editionContainer.SetActive(false);
        sealContainer.SetActive(false);
        extraTags[0].extraTagBg.gameObject.SetActive(false);
        extraTags[1].extraTagBg.gameObject.SetActive(false);
        extraTags[2].extraTagBg.gameObject.SetActive(false);
        if (descriptionType == DescriptionType.None)
        {
            extraTagContainer.extraTagBg.gameObject.SetActive(false);
            return;
        }


        extraTagContainer.extraTagBg.gameObject.SetActive(true);
        DescriptionColor descriptionColor = DatabaseManager.instance.cardColorDatabase.descriptionColors.Find(x => x.descriptionType == descriptionType);

        extraTagContainer.extraTagBg.color = descriptionColor.instanceColor;
        if (auxText != string.Empty)
        {
            extraTagContainer.extraTagText.text = auxText;

        }
        else
        {
            extraTagContainer.extraTagText.text = descriptionColor.descriptionType.ToString();
        }
    }

    private void SetEditionDescription(CardEdition cardEdition)
    {
        if (cardEdition == CardEdition.Base)
        {
            editionContainer.SetActive(false);
            return;
        }
        if (cardEdition == CardEdition.Foil)
        {
            editionDescription.text = "<style=Chips>+50</style> Chips when scored";
        }

        if (cardEdition == CardEdition.Holographic)
        {
            editionDescription.text = "<style=Mult>+10</style> Mult when scored";
        }

        if (cardEdition == CardEdition.Polychrome)
        {
            editionDescription.text = "<style=Mult>X1.5</style> Mult when scored";
        }

        if (cardEdition == CardEdition.Negative)
        {
            editionDescription.text = "<style=Info>+1</style> Joker Slot";
        }

        editionTitle.text = cardEdition.ToString();

        editionContainer.SetActive(true);
    }

    private void SetSealDescription(Seal seal)
    {
        if (seal == Seal.None)
        {
            sealContainer.SetActive(false);
            return;
        }
        if (seal == Seal.Gold)
        {
            sealDescription.text = "Earn <style=Cash>$3</style> when this card is played and scores";
        }

        if (seal == Seal.Red)
        {
            sealDescription.text = "Retrigger this card <style=Mult>1</style> time";
        }

        if (seal == Seal.Blue)
        {
            sealDescription.text = "Creates the <style=Planet>Planet</style> card for final played poker hand of round if held in hand (Must have room)";
        }

        if (seal == Seal.Purple)
        {
            sealDescription.text = "Creates a <style=Tarot>Tarot</style> card when discarded (Must have room)";
        }

        sealTitle.text = seal.ToString() + " Seal";

        sealContainer.SetActive(true);
    }
}

[System.Serializable]
public class ExtraTagContainer
{
    public TextMeshProUGUI extraTagText;
    public Image extraTagBg;
}
public enum DescriptionType
{
    None = -1,
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Legendary = 3,
    Booster = 4,
    Planet = 5,
    Tarot = 6,
    Spectral = 7,
    Voucher = 8,
    Special_Card = 9,
    Gold_Seal = 10,
}
