using UnityEngine;
[CreateAssetMenu(fileName = "Extra Live",menuName ="Scriptables/Joker/Effect/Set up/Extra Live")]
public class AddExtraLive : JokerEffect
{
    public int liveAmmount;
    public override void ApplyEffect(JokerInstance instance)
    {
        CardPlayer.instance.extraLives += liveAmmount;
    }
}
