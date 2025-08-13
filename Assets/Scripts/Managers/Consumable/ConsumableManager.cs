using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject consumablePrefab;

    [Header("Testing")]
    [SerializeField] private PlanetCardData generatePlanetCard;

    public static ConsumableManager instance;

    void Awake()
    {
        instance = this;
    }

    public void GenerateConsumableOnSlot(PackType packType)
    {
        if (packType == PackType.Planet)
        {
            GameObject go = Instantiate(consumablePrefab, spawnParent);

            go.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard());
        }
    }

    [ContextMenu("Generate Planet Card")]
    private void GeneratePlanetCard()
    {
        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetPlanetData(generatePlanetCard);

    }

    public void GeneratePlanetCard(HandType handType)
    {
        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetPlanetCardByHandType(handType));

    }

}
