using TMPro;
using UnityEngine;

public class JokerDescription : MonoBehaviour
{
    public static JokerDescription instance;

    void Awake()
    {
        instance = this;
        SetDescriptionOff();
    }
    [SerializeField] private TextMeshProUGUI nameText, descriptionText;

    [SerializeField] private float cardDescriptionOffsetY;
    public void SetDescriptionOn(JokerData jokerData, Vector2 jokerPos)
    {
        transform.localScale = Vector2.one;
        nameText.text = jokerData.jokerName;
        descriptionText.text = jokerData.description;
        transform.position = jokerPos;
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
    }
}
