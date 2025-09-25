using UnityEngine;

public class TagBehaviour : MonoBehaviour
{
    [SerializeField] private TagData testTag;
    public void SetTagData(TagData tagData)
    {
        GetComponent<DescriptionContainer>().SetNameAndDescription(tagData.tagName, tagData.tagDescription, DescriptionType.None);
    }
    [ContextMenu("Test Tag")]
    private void TestTag()
    {
        SetTagData(testTag);
    }
}
