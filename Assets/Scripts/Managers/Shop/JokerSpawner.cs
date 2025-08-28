using System.Collections;
using UnityEngine;

public class JokerSpawner : MonoBehaviour
{
    [SerializeField] private int shopItemSlots = 2;
    [Header("Joker Spawning")]
    [SerializeField] private Transform jokerTransform;
    [SerializeField] private GameObject jokerPrefab;
    [SerializeField] private GameObject planetCardprefab;

    [Header("Packs Database")]
    [SerializeField] private Transform packsTransform;
    [SerializeField] private GameObject shopPackPrefab;
    void OnEnable()
    {
        StartCoroutine(GenerateStuff());
    }

    private IEnumerator GenerateStuff()
    {
        yield return new WaitForSeconds(0.1f);
        GenerateItems();
    }
    public void GenerateItems()
    {
        ClearItemsInShop();

        for (int i = 0; i < shopItemSlots; i++)
        {
            int randomChoice = Random.Range(0, 100);
            if (randomChoice < 71)
            {
                GameObject currentJoker = Instantiate(jokerPrefab, jokerTransform);
                currentJoker.GetComponent<JokerContainer>().SetUpJoker(DatabaseManager.instance.jokerContainer.GetRandomJoker());
            }
            else
            {
                GameObject planetCard = Instantiate(planetCardprefab, jokerTransform);
                planetCard.GetComponent<ConsumableItem>().isOnShop = true;
                planetCard.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard());
            }
        }

        gameObject.SetActive(false);
    }

    private void ClearItemsInShop()
    {
        Transform[] existingItems = jokerTransform.GetComponentsInChildren<Transform>();
        if (existingItems.Length > 1)
        {
            for (int i = 1; i < existingItems.Length; i++)
            {
                Destroy(existingItems[i].gameObject);
            }
        }
    }
    public void GenerateShopPacks()
    {
        Transform[] packs = packsTransform.GetComponentsInChildren<Transform>();
        if (packs.Length > 1)
        {
            for (int i = 1; i < packs.Length; i++)
            {
                Destroy(packs[i].gameObject);
            }
        }


        for (int i = 0; i < 2; i++)
        {
            GameObject shopPack = Instantiate(shopPackPrefab, packsTransform);
            shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomPack());
        }
    }

    [ContextMenu("Generate All Shop")]
    private void GenerateAllShop()
    {
        GenerateShopPacks();
        GenerateStuff();
    }
}
