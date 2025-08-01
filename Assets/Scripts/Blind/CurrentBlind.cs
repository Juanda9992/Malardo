using UnityEngine;

public abstract class CurrentBlind : ScriptableObject
{
    public string blindName;
    public string blindDescription;
    public abstract void ApplyEffect();
    public abstract void RevertEffect();
}
