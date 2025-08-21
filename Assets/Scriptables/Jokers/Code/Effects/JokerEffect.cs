using UnityEngine;

public abstract class JokerEffect : ScriptableObject
{
    public float ammount;
    public virtual void ApplyEffect(){}

    public string jokerOutput;

    public virtual string GetCustomMessage()
    {
        return string.Empty;
    }
    public virtual int CheckForActivation(Card card)
    {
        return 1;
    }

    public virtual void ApplyEffect(JokerInstance instance)
    {

    }
}
