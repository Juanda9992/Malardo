using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Packs Database", menuName = "Scriptables/Database/Shop Packs Database")]
public class PackDatabase : ScriptableObject
{
    public List<PackData> buffonPacks;
    public List<PackData> cardPacks;
    public List<PackData> planetPacks;

    public PackData GetRandomBuffonPack()
    {
        return buffonPacks[Random.Range(0, buffonPacks.Count)];
    }

    public PackData GetRandomCardPack()
    {
        return cardPacks[Random.Range(0, cardPacks.Count)];
    }

    public PackData GetRandomPlanetPack()
    {
        return planetPacks[Random.Range(0, planetPacks.Count)];
    }

    public PackData GetRandomPack()
    {
        int packIndex = Random.Range(0, 3);
        if (packIndex == 0)
        {
            return GetRandomBuffonPack();
        }
        else if (packIndex == 1)
        {
            return GetRandomCardPack();
        }
        else
        {
            return GetRandomPlanetPack();
        }

    }
}
