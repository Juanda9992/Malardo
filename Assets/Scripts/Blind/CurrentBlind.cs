using UnityEngine;
using System.Collections;
public abstract class CurrentBlind : ScriptableObject
{
    public string blindName;
    [TextArea]public string blindDescription;
    public float blindMultiplier;
    public Sprite blindSprite;
    public Color blindColor;
    public abstract void ApplyEffect();
    public abstract void RevertEffect();

    public virtual IEnumerator CheckEffect()
    {
        yield return null;
    }
    [ContextMenu("Force Select")]
    protected void ForceBlindSelect()
    {
        BlindManager.instance.SetUpBossBlind(this);
    }
}
