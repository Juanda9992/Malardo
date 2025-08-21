using UnityEngine;

[CreateAssetMenu(fileName = "SetPackPrice",menuName = "Scriptables/Joker/Effect/Misc/Set Pack Price")]
public class SetPackPrice : JokerEffect
{

    public bool freePlanetPacks;
    public override void ApplyEffect(JokerInstance jokerInstance)
    {
        DatabaseManager.instance.pricesDatabase.freePlanetPacks = freePlanetPacks;
    }
}
