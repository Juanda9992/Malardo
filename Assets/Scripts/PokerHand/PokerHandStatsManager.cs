using System.Collections.Generic;
using UnityEngine;

public class PokerHandStatsManager : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject statsPrefab;

    private List<PokerHandLevelData> pokerHands;
    private List<PokerHandStatsVisuals> pokerHandStatsVisuals = new List<PokerHandStatsVisuals>();
    void Start()
    {
        SpawnStats();
    }

    private void SpawnStats()
    {
        pokerHands = PokerHandLevelStorage.instance.GetPokerHands();
        for (int i = 0; i < pokerHands.Count; i++)
        {
            GameObject go = Instantiate(statsPrefab, spawnParent);
            go.GetComponent<PokerHandStatsVisuals>().SetUpData(pokerHands[i]);

            pokerHandStatsVisuals.Add(go.GetComponent<PokerHandStatsVisuals>());
        }

        UpdateStats();
    }

    public void UpdateStats()
    {
        pokerHands = PokerHandLevelStorage.instance.GetPokerHands();

        for (int i = 0; i < pokerHandStatsVisuals.Count; i++)
        {
            pokerHandStatsVisuals[i].SetUpData(pokerHands[i]);
        }
    }
}
