using UnityEngine;

public abstract class CurrentBlind : ScriptableObject
{
    public string blindName;
    public string blindDescription;
    public float blindMultiplier;
    public Sprite blindSprite;
    public Color blindColor;
    public abstract void ApplyEffect();
    public abstract void RevertEffect();
}
