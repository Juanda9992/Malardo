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
        descriptionText.text = $"+{card_Data.currentCard.chipAmmount} chips";
        SetDescriptionRarity(DescriptionType.None);
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
    private void SetDescriptionRarity(DescriptionType descriptionType)
    {
        if (descriptionType == DescriptionType.None)
        {
            rarityObj.SetActive(false);
            return;
        }


        rarityObj.SetActive(true);
        DescriptionColor descriptionColor = DatabaseManager.instance.cardColorDatabase.descriptionColors.Find(x => x.descriptionType == descriptionType);

        rarityBgColor.color = descriptionColor.instanceColor;
        rarityText.text = descriptionColor.descriptionType.ToString();
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
    Voucher = 8
}
