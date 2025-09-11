using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Packs Database", menuName = "Scriptables/Database/Shop Packs Database")]
public class PackDatabase : ScriptableObject
{
    public List<PackData> buffonPacks;
    public List<PackData> cardPacks;
    public List<PackData> planetPacks;
    public List<PackData> tarotPacks;
    public List<PackData> spectralPacks;
    public PackData GetRandomBuffonPack(PackSize packSize)
    {
        List<PackData> packsGathered = buffonPacks.FindAll(x => x.packSize == packSize);
        return packsGathered[Random.Range(0, packsGathered.Count)];
    }

    public PackData GetRandomCardPack(PackSize packSize)
    {
        List<PackData> packsGathered = cardPacks.FindAll(x => x.packSize == packSize);
        return packsGathered[Random.Range(0, packsGathered.Count)];
    }

    public PackData GetRandomPlanetPack(PackSize packSize)
    {
        List<PackData> packsGathered = planetPacks.FindAll(x => x.packSize == packSize);
        return packsGathered[Random.Range(0, packsGathered.Count)];
    }

    public PackData GetRandomArcanaPack(PackSize packSize)
    {
        List<PackData> packsGathered = tarotPacks.FindAll(x => x.packSize == packSize);
        return packsGathered[Random.Range(0, packsGathered.Count)];
    }

    public PackData GetRandomSpectralPack(PackSize packSize)
    {
        List<PackData> packsGathered = spectralPacks.FindAll(x => x.packSize == packSize);
        return packsGathered[Random.Range(0, packsGathered.Count)];
    }
}
public enum PackSize
{
    Normal,
    Jumbo,
    Mega
}
