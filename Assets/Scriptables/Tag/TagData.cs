using UnityEngine;

[CreateAssetMenu(fileName = "Tag Data", menuName = "Scriptables/Tag/Template")]
public class TagData : ScriptableObject
{
    public string tagName;
    [TextArea] public string tagDescription;
    public Sprite tagSprite;
    public CardEffect tagEffect;
    public TagExchangeMoment tagExchangeMoment;

    public string GetFormatedDescription()
    {
        return tagEffect.GetDescription(tagDescription);
    }

}

public enum TagExchangeMoment
{
    Instant,
    Shop,
    Game
}
