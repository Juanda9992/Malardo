using UnityEngine;
[CreateAssetMenu(fileName = "Generate Mega Pack",menuName ="Scriptables/Tag/Effect/Generate Mega Pack")]
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
        }
    }
}
