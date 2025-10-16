using UnityEngine;
[CreateAssetMenu(fileName = "Duplicate Tag",menuName = "Scriptables/Tag/Effect/Duplicate Tag")]
public class DuplicateTag : CardEffect
{
    public override void ApplyEffect()
    {
        BlindSkipper.instance.GenerateLastTag();
    }
}
