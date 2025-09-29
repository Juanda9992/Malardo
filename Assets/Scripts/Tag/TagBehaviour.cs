using UnityEngine;

public class TagBehaviour : MonoBehaviour
{
    [SerializeField] private TagData testTag;

    private TagData currentTag;
    public void SetTagData(TagData tagData)
    {
        currentTag = tagData;
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
