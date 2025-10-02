using UnityEngine;
using UnityEngine.UI;

public class TagBehaviour : MonoBehaviour
{
    [SerializeField] private TagData testTag;
    [SerializeField] private Image tagSprite;
    [SerializeField] private DescriptionContainer descriptionContainer;
    private TagData currentTag;
    public void SetTagData(TagData tagData)
    {
        currentTag = tagData;
        tagSprite.sprite = currentTag.tagSprite;
        descriptionContainer.SetNameAndDescription(tagData.tagName, tagData.GetFormatedDescription(), DescriptionType.None);
    }
    public void UpdateDescription()
    {
        descriptionContainer.SetNameAndDescription(currentTag.tagName, currentTag.GetFormatedDescription(), DescriptionType.None);
    }

    public TagData GetCurrentTag()
    {
        return currentTag;
    }
    [ContextMenu("Test Tag")]
    private void TestTag()
    {
        SetTagData(testTag );
    }
}
