using UnityEngine;
[CreateAssetMenu(fileName = "Planet Card", menuName = "Scriptables/Database/Planet Card")]
public class PlanetCardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string cardDescription;
    public HandType handType;
}
