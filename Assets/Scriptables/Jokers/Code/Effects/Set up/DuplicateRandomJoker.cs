using UnityEngine;
[CreateAssetMenu(fileName = "Duplicate Random Joker",menuName ="Scriptables/Joker/Effect/Misc/Duplicate Random Joker")]
public class DuplicateRandomJoker : JokerEffect
{
    public override void ApplyEffect(JokerInstance instance)
    {
        if (instance.dynamicVariable == 2)
        {
            JokerManager.instance.StartCoroutine(JokerManager.instance.DuplicateRandomJoker());
        }
    }
}
