using UnityEngine;

[CreateAssetMenu(fileName = "Tarot Card", menuName = "Scriptables/Tarot Card/Template")]
public class TarotCardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string cardDescription;
    public CardEffect cardEffect;
    public bool saveCard;
    public bool CanApplyEffect()
    {
        return cardEffect.CanBeUsed();
    }

    public void SaveCard()
    {
        CardManager.instance.UpdateLastCard(this);
    }
    public string GetDescription()
    {
        Debug.Log(cardEffect.GetDescription(cardDescription));
        return cardEffect.GetDescription(cardDescription);
    }
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

    public virtual string GetDescription(string baseDescription)
    {
        return baseDescription;
    }

    public virtual bool CanBeUsed()
    {
        return true;
    }
}
