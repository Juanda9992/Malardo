using UnityEngine;

[CreateAssetMenu(fileName = "Set Doscards", menuName = "Scriptables/Joker/Effect/Set up/Set Doscards")]
public class SetDiscards : JokerEffect
{
    public int discardAmmount;
    public bool addAmmount;
    public override void ApplyEffect()
    {
        if (addAmmount)
        {
            HandManager.instance.SetDefaultDiscards(HandManager.instance.defaultDiscards + discardAmmount);
        }
        else
        {
            HandManager.instance.SetDefaultDiscards(discardAmmount);
        }
    }
}
