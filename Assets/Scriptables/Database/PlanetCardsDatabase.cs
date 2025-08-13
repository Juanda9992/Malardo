using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet Card Database", menuName = "Scriptables/Database/Planet Card Database")]
public class PlanetCardsDatabase : ScriptableObject
{
    public List<PlanetCardData> allPlanetCards;

    public PlanetCardData GetRandomPlanetCard()
    {
        return allPlanetCards[Random.Range(0, allPlanetCards.Count)];
    }

    public PlanetCardData GetPlanetCardByHandType(HandType handType)
    {
        return allPlanetCards.Find(x => x.handType == handType);
    }
}
