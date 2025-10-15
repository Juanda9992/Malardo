using UnityEngine;
using UnityEngine.UI;

public class TagBehaviour : MonoBehaviour
{
    [SerializeField] private TagData testTag;
    [SerializeField] private Image tagSprite;
    [SerializeField] private DescriptionContainer descriptionContainer;
    private TagData currentTag;

    public HandType handType;
    public void SetTagData(TagData tagData, HandType lastHand = HandType.None)
    {
        currentTag = tagData;

        if (currentTag.useHandType)
        {
            handType = CommonOperations.GetRandomHandType();
        }

        if(lastHand != HandType.None)
        {
            handType = lastHand;
        }

        tagSprite.sprite = currentTag.tagSprite;
        descriptionContainer.SetNameAndDescription(tagData.tagName, tagData.GetFormatedDescription(), DescriptionType.None);
    }
    public void UpdateDescription()
    {
        if (currentTag.useHandType)
        {
            descriptionContainer.SetNameAndDescription(currentTag.tagName, currentTag.GetFormatedDescription(handType), DescriptionType.None);
        }
        else
        {
            descriptionContainer.SetNameAndDescription(currentTag.tagName, currentTag.GetFormatedDescription(), DescriptionType.None);
        }
    }

    public void ApplyEffect()
    {
        if (currentTag.useHandType)
        {
            currentTag.tagEffect.ApplyEffect(handType);
        }
        else
        {
            currentTag.tagEffect.ApplyEffect();
        }
    }

    public TagData GetCurrentTag()
    {
        return currentTag;
    }
    [ContextMenu("Test Tag")]
    private void TestTag()
    {
        SetTagData(testTag);
    }
}
