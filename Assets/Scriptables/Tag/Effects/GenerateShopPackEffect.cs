using UnityEngine;
[CreateAssetMenu(fileName = "Generate Mega Pack",menuName ="Scriptables/Tag/EffectGenerate Mega Pack")]
public class GenerateShopPackEffect : CardEffect
{
    [SerializeField] private PackType packToGenerate;

    public override void ApplyEffect()
    {
        switch (packToGenerate)
        {
            case PackType.Spectral:
                PackManager.instance.ReceiveCreatePackInstruction(DatabaseManager.instance.shopPacksDatabase.GetRandomSpectralPack(PackSize.Mega));
                break;
                case PackType.Tarot:
                PackManager.instance.ReceiveCreatePackInstruction(DatabaseManager.instance.shopPacksDatabase.GetRandomArcanaPack(PackSize.Mega));
                break;
                case PackType.Card:
                PackManager.instance.ReceiveCreatePackInstruction(DatabaseManager.instance.shopPacksDatabase.GetRandomCardPack(PackSize.Mega));
                break;
                case PackType.Planet:
                PackManager.instance.ReceiveCreatePackInstruction(DatabaseManager.instance.shopPacksDatabase.GetRandomPlanetPack(PackSize.Mega));
                break;
                case PackType.Buffon:
                PackManager.instance.ReceiveCreatePackInstruction(DatabaseManager.instance.shopPacksDatabase.GetRandomBuffonPack(PackSize.Mega));
                break;
        }
    }
}
