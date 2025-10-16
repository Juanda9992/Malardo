using UnityEngine;
[CreateAssetMenu(fileName = "Generate Tag",menuName ="Scriptables/Joker/Effect/Set up/Create Tag")]
public class GenerateTagEffect : JokerEffect
{
    [SerializeField] private TagData tagData;
    public override void ApplyEffect(JokerInstance instance)
    {
        TagGenerator.instance.CreateTag(tagData);
    }
}
