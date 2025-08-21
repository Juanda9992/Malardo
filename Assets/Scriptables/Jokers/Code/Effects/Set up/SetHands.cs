using UnityEngine;

[CreateAssetMenu(fileName = "Set Doscards", menuName = "Scriptables/Joker/Effect/Set up/Set Hands")]
public class SetHands : JokerEffect
{
    public int handAmmount;
    public bool addAmmount;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        if (addAmmount)
        {
            HandManager.instance.SetDefaultHands(HandManager.instance.defaultHands+ handAmmount);
        }
        else
        {
            HandManager.instance.SetDefaultHands(handAmmount);
        }
    }
}
