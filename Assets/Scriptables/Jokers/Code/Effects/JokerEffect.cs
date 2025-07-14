using UnityEngine;

public abstract class JokerEffect : ScriptableObject
{
    public float ammount;
    public abstract void ApplyEffect();

    public string jokerOutput;

    public virtual string GetCustomMessage()
    {
        return string.Empty;
    }
}
