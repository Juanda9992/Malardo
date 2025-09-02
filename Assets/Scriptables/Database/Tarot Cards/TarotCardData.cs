using UnityEngine;

[CreateAssetMenu(fileName = "Tarot Card",menuName = "Scriptables/Tarot Card/Template")]
public class TarotCardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string cardDescription;

    public int minCardsRequired, maxCardsRequired;

    public CardEffect cardEffect;
}

[CreateAssetMenu(fileName = "Tarot Card Effect", menuName = "Scriptables/Tarot Card/Effect")]
public class CardEffect : ScriptableObject
{
    public virtual void ApplyEffect()
    {
        Debug.Log("Applied");
    }

    [ContextMenu("Apply Effect")]
    protected void TestEffect()
    {
        ApplyEffect();
    }
}
