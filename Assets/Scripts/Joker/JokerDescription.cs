using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerDescription : MonoBehaviour
{
    public static JokerDescription instance;

    [SerializeField] private TextMeshProUGUI nameText, descriptionText;
    [SerializeField] private float cardDescriptionOffsetY;
    [SerializeField] private Image rarityBgColor;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private GameObject rarityObj;
    void Awake()
    {
        instance = this;
        SetDescriptionOff();
    }
    public void SetDescriptionOff()
    {
        transform.localScale = Vector2.zero;
    }

    public void SetCardDescription(Card_Data card_Data)
    {
        nameText.text = card_Data.currentCard.cardName;
        transform.localScale = Vector2.one;
        transform.position = (Vector2)card_Data.transform.position + new Vector2(0, cardDescriptionOffsetY);
        descriptionText.text = $"<style=Chips>+{card_Data.currentCard.chipAmmount} </style>chips";
        SetCardData(card_Data);
        StartCoroutine("ForceRebuildDesc");
    }

    private void SetCardData(Card_Data card_Data)
    {
        if (card_Data.currentCard.cardType == CardType.Default)
        {
            SetDescriptionRarity(DescriptionType.None);
            return;
        }

        switch (card_Data.currentCard.cardType)
        {
            case CardType.Glass:
                descriptionText.text += '\n' + "<style=Mult>X2 Mult</style> \n 1 in 4 chance to destroy card";
                SetDescriptionRarity(DescriptionType.Special_Card, "Glass Card");
                break;

            case CardType.Stone:
                descriptionText.text += '\n' + "no rank or suit";
                SetDescriptionRarity(DescriptionType.Special_Card, "Stone Card");
                break;
            case CardType.Lucky:
                descriptionText.text += '\n' + "1 in 5 chance \n for <style=Mult>+20 mult</style>\n1 in 15 chance \n to win <style=Cash>$20</style> ";
                SetDescriptionRarity(DescriptionType.Special_Card, "Lucky Card");
                break;
            case CardType.Silver:
                descriptionText.text += '\n' + "<style=Mult>X1.5 Mult</style> \n while this card \n stays in hand";
                SetDescriptionRarity(DescriptionType.Special_Card, "Silver Card");
                break;
            case CardType.Gold:
                descriptionText.text += '\n' + "<style=Cash>$3</style> if this \n card is held in hand \n at the end of the round";
                SetDescriptionRarity(DescriptionType.Special_Card, "Gold Card");
                break;
            case CardType.Bonus:
                descriptionText.text += '\n' + "<style=Chips>+30</style> Extra Chips";
                SetDescriptionRarity(DescriptionType.Special_Card, "Bonus Card");
                break;
            case CardType.Mult:
                descriptionText.text += '\n' + "<style=Mult>+4</style> Mult";
                SetDescriptionRarity(DescriptionType.Special_Card, "Mult Card");
                break;



        }
    }

    public void SetGenericDescription(string itemName, string itemDescription, Vector2 itemPosition, DescriptionType descriptionType)
    {
        nameText.text = itemName;
        descriptionText.text = itemDescription;
        transform.position = itemPosition;
        transform.localScale = Vector2.one;

        SetDescriptionRarity(descriptionType);

        StartCoroutine("ForceRebuildDesc");
    }
    private IEnumerator ForceRebuildDesc()
    {
        descriptionText.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        descriptionText.gameObject.SetActive(true);

    }
    private void SetDescriptionRarity(DescriptionType descriptionType, string auxText = "")
    {
        if (descriptionType == DescriptionType.None)
        {
            rarityObj.SetActive(false);
            return;
        }


        rarityObj.SetActive(true);
        DescriptionColor descriptionColor = DatabaseManager.instance.cardColorDatabase.descriptionColors.Find(x => x.descriptionType == descriptionType);

        rarityBgColor.color = descriptionColor.instanceColor;
        if (auxText != string.Empty)
        {
            rarityText.text = auxText;

        }
        else
        {
            rarityText.text = descriptionColor.descriptionType.ToString();
        }
    }


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
    Special_Card = 9
}
