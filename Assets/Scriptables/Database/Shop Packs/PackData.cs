using UnityEngine;

[CreateAssetMenu(fileName = "Shop Pack", menuName = "Scriptables/Database/Shop/Shop Pack")]
public class PackData : ScriptableObject
{
    public string packName;
    public string packDescription;
    public int packCost;
    public PackType packType;
    public int numberOfCards;
    public int selectCards;
}
