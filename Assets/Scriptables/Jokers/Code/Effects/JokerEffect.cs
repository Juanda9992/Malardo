using UnityEngine;

public abstract class JokerEffect : ScriptableObject
{
    public float ammount;

    public virtual string GetCustomMessage()
    {
        return string.Empty;
    }
    public virtual int CheckForActivation(Card card)
    {
        return 1;
    }

    public abstract void ApplyEffect(JokerInstance instance);

    public virtual void UpdateDescription(JokerInstance instance)
    {
    }

    public virtual void SetupEffect(JokerInstance jokerInstance)
    {

    }

    public virtual bool Scores(JokerInstance instance)
    {
        return true;
    }
}
