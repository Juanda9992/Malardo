using UnityEngine;
using UnityEngine.UI;

public class TagBehaviour : MonoBehaviour
{
    [SerializeField] private TagData testTag;
    [SerializeField] private Image tagSprite;
    private TagData currentTag;
    public void SetTagData(TagData tagData)
    {
        currentTag = tagData;
        tagSprite.sprite = currentTag.tagSprite;
        GetComponent<DescriptionContainer>().SetNameAndDescription(tagData.tagName, tagData.tagDescription, DescriptionType.None);
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
