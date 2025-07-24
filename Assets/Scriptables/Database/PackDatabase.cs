using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Packs Database", menuName = "Scriptables/Database/Shop Packs Database")]
public class PackDatabase : ScriptableObject
{
    public List<PackData> buffonPacks;

    public PackData GetRandomBuffonPack()
    {
        return buffonPacks[Random.Range(0, buffonPacks.Count)];
    }
}
