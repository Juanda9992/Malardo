using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Packs Database", menuName = "Scriptables/Database/Shop Packs Database")]
public class PackDatabase : ScriptableObject
{
    public List<PackData> buffonPacks;
    public List<PackData> cardPacks;

    public PackData GetRandomBuffonPack()
    {
        return buffonPacks[Random.Range(0, buffonPacks.Count)];
    }

    public PackData GetRandomCardPack()
    {
        return cardPacks[Random.Range(0, cardPacks.Count)];
    }

    public PackData GetRandomPack()
    {
        int packIndex = Random.Range(0, 2);
        if (packIndex == 0)
        {
            return GetRandomBuffonPack();
        }
        else
        {
            return GetRandomCardPack();
        }

    }
}
