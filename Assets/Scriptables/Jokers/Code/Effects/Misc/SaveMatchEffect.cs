using UnityEngine;
[CreateAssetMenu(fileName = "Self Destroy",menuName = "Scriptables/Joker/Effect/Set up/Self Destroy")]
public class SaveMatchEffect : JokerEffect
{
    public string destroyMessage;
    public override void ApplyEffect(JokerInstance instance)
    {
        ScoreManager.instance.saved = false;
        CardPlayer.instance.extraLives--;
        instance.triggerMessage = destroyMessage;
        JokerManager.instance.RemoveJoker(instance.jokerContainer);
    }
}
