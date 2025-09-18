using UnityEngine;

public class DescriptionContainer : MonoBehaviour
{
    [SerializeField, TextArea] private string description;
    [SerializeField] private string contentName;

    [SerializeField] private Vector2 descriptionOffset;
    private DescriptionType descriptionType;
    private CardEdition cardEdition;
    public void ShowDescription()
    {
        JokerDescription.instance.SetGenericDescription(contentName, description, (Vector2)transform.position + descriptionOffset, descriptionType, cardEdition);
    }

    public void HideDescription()
    {
        JokerDescription.instance.SetDescriptionOff();
    }

    public void SetNameAndDescription(string itemName, string itemDescription, DescriptionType _descriptionType, CardEdition _cardEdition = CardEdition.Base)
    {
        description = itemDescription;
        contentName = itemName;
        descriptionType = _descriptionType;
        cardEdition = _cardEdition;
    }
}
