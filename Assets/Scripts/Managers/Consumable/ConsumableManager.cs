using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject consumablePrefab;

    [SerializeField] private int maxConsumables = 2;
    [SerializeField] private TextMeshProUGUI consumableSlotText;
    [Header("Testing")]
    [SerializeField] private PlanetCardData generatePlanetCard;
    [SerializeField] private TarotCardData generateTarot;

    public static ConsumableManager instance;

    public bool CanAddConsumable { get { return consumableAmmount < maxConsumables; } }

    private int consumableAmmount;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateConsumableText();
    }
    private void UpdateConsumableText()
    {
        consumableSlotText.text = consumableAmmount + " / " + maxConsumables;
    }

    public void GenerateConsumableOnSlot(PackType packType)
    {
        if (consumableAmmount >= maxConsumables) return;

        if (packType == PackType.Planet)
        {
            GameObject go = Instantiate(consumablePrefab, spawnParent);
            go.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard());
        }

        IncreaseConsumables();
    }

    [ContextMenu("Generate Planet Card")]
    private void GeneratePlanetCard()
    {
        if (consumableAmmount >= maxConsumables) return;

        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetPlanetData(generatePlanetCard);

        IncreaseConsumables();
    }

    [ContextMenu("Generate Tarot")]
    private void GenerateTarotCard()
    {
        if (consumableAmmount >= maxConsumables) return;

        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetTarotData(generateTarot);

        IncreaseConsumables();
    }

    public void GeneratePlanetCard(HandType handType)
    {
        if (consumableAmmount >= maxConsumables) return;

        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetPlanetCardByHandType(handType));
        IncreaseConsumables();
    }

    public void GeneratePlanetCard(PlanetCardData planetCardData)
    {
        if (consumableAmmount >= maxConsumables) return;
        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetPlanetData(planetCardData);
        IncreaseConsumables();
    }

    public void GenerateTarotCard(TarotCardData tarotCard)
    {
        if (consumableAmmount >= maxConsumables) return;
        GameObject go = Instantiate(consumablePrefab, spawnParent);
        go.GetComponent<ConsumableItem>().SetTarotData(tarotCard);
        IncreaseConsumables();
    }


    public void AddFromShop(ConsumableItem consumable)
    {
        consumable.transform.SetParent(spawnParent);
        consumable.isOnShop = false;
        consumable.SetUpButtons();

        IncreaseConsumables();
    }

    public void IncreaseConsumables()
    {
        consumableAmmount++;
        UpdateConsumableText();
    }

    public void DecreaseConsumable()
    {
        consumableAmmount--;
        UpdateConsumableText();
    }

}
