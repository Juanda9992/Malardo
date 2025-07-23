using UnityEngine;

public class DescriptionContainer : MonoBehaviour
{
    [SerializeField, TextArea] private string description;
    [SerializeField] private string contentName;

    [SerializeField] private Vector2 descriptionOffset;
    public void ShowDescription()
    {
        JokerDescription.instance.SetGenericDescription(contentName, description, (Vector2)transform.position + descriptionOffset);
    }

    public void HideDescription()
    {
        JokerDescription.instance.SetDescriptionOff();
    }
}
