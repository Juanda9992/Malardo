using UnityEngine;

public abstract class JokerEffect : ScriptableObject
{
    public int ammount;
    public abstract void ApplyEffect();

    public string jokerOutput;

    public virtual string GetCustomMessage()
    {
        return string.Empty;
    }
}
