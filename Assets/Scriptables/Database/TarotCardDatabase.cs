using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tarot Card Dabatase", menuName = "Scriptables/Database/Tarot Card Database")]
public class TarotCardDatabase : ScriptableObject
{
    public List<TarotCardData> tarotCards;
    public List<TarotCardData> spectralCards;
    public TarotCardData GetRandomTarotCard()
    {
        return tarotCards[Random.Range(0, tarotCards.Count)];
    }

    public TarotCardData GetRandomSpectralCard()
    {
        return spectralCards[Random.Range(0,spectralCards.Count)];
    }
}
