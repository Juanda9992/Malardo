using UnityEngine;

public class DescriptionContainer : MonoBehaviour
{
    [SerializeField, TextArea] private string description;
    [SerializeField] private string contentName;

    [SerializeField] private Vector2 descriptionOffset;
    private DescriptionType descriptionType;
    public void ShowDescription()
    {
        JokerDescription.instance.SetGenericDescription(contentName, description, (Vector2)transform.position + descriptionOffset,descriptionType);
    }

    public void HideDescription()
    {
        JokerDescription.instance.SetDescriptionOff();
    }

    public void SetNameAndDescription(string itemName, string itemDescription, DescriptionType _descriptionType)
    {
        description = itemDescription;
        contentName = itemName;
        descriptionType = _descriptionType;
    }
}
