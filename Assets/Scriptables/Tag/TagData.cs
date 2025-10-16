using UnityEngine;

[CreateAssetMenu(fileName = "Tag Data", menuName = "Scriptables/Tag/Template")]
public class TagData : ScriptableObject
{
    public string tagName;
    [TextArea] public string tagDescription;
    public Sprite tagSprite;
    public CardEffect tagEffect;
    public TagExchangeMoment tagExchangeMoment;

    public bool useHandType;

    public string GetFormatedDescription()
    {
        return tagEffect.GetDescription(tagDescription);
    }
    public string GetFormatedDescription(HandType handType)
    {
        return tagEffect.GetDescription(tagDescription,handType);
    }

}

public enum TagExchangeMoment
{
    Instant,
    Shop,
    Game,
    Duplicate
}
