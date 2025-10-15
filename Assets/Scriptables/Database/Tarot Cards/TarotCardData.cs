using UnityEngine;

[CreateAssetMenu(fileName = "Tarot Card", menuName = "Scriptables/Tarot Card/Template")]
public class TarotCardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string cardDescription;
    public CardEffect cardEffect;
    public bool isTarot;
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

    public virtual void ApplyEffect(HandType var)
    {
           
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

    public virtual string GetDescription(string baseDescription,HandType handType)
    {
        return baseDescription.Replace("_R_",CommonOperations.ParseHandType(handType));
    }
    public virtual bool CanBeUsed()
    {
        return true;
    }
}
