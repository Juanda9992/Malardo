using UnityEngine;
[CreateAssetMenu(fileName = "Joker Operation",menuName = "Scriptables/Tarot Card/Effect/Joker Operation")]
public class JokerOperations : CardEffect
{
    public bool copyJoker;
    public override void ApplyEffect()
    {
        if (copyJoker)
        {
            JokerManager.instance.SetCopyRandomJokerCoroutine();
        }
    }
    public override bool CanBeUsed()
    {
        return JokerManager.instance.currentJokers.Count > 0;
    }
}
