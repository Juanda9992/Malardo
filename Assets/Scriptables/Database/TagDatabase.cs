using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tag database", menuName = "Scriptables/Database/Tag Database")]
public class TagDatabase : ScriptableObject
{
    public List<TagData> allTags;

    public TagData GetRandomTag()
    {
        return allTags[Random.Range(0, allTags.Count)];
    }
}
