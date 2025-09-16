using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JokerSpawner : MonoBehaviour
{
    [Header("Joker Spawning")]
    [SerializeField] private HorizontalLayoutGroup jokerTransform;
    [SerializeField] private GameObject jokerPrefab;
    [SerializeField] private GameObject consumableCardprefab;

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
        CommonOperations.DestroyChildsInParent(jokerTransform.transform);

        for (int i = 0; i < ShopManager.instance.maxItemsOnShop; i++)
        {
            int randomChoice = Random.Range(0, 100);
            if (randomChoice < 71)
            {
                GameObject currentJoker = Instantiate(jokerPrefab, jokerTransform.transform);
                currentJoker.GetComponent<JokerContainer>().SetUpJoker(DatabaseManager.instance.jokerContainer.GetRandomJoker());
            }
            else if (randomChoice < 85)
            {
                GameObject consumableCard = Instantiate(consumableCardprefab, jokerTransform.transform);
                consumableCard.GetComponent<ConsumableItem>().isOnShop = true;
                consumableCard.GetComponent<ConsumableItem>().SetPlanetData(DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard());
            }
            else
            {
                GameObject consumableCard = Instantiate(consumableCardprefab, jokerTransform.transform);

                consumableCard.GetComponent<ConsumableItem>().isOnShop = true;
                consumableCard.GetComponent<ConsumableItem>().SetTarotData(DatabaseManager.instance.tarotCardDatabase.GetRandomTarotCard());
            }
        }

        CommonOperations.UpdateCardSpacing(jokerTransform.transform, jokerTransform,2);

        gameObject.SetActive(false);
    }
    public void GenerateShopPacks()
    {
        CommonOperations.DestroyChildsInParent(packsTransform);
        PackSize packSize;
        for (int i = 0; i < 2; i++)
        {
            GameObject shopPack = Instantiate(shopPackPrefab, packsTransform);
            int randomPack = Random.Range(0, 100);
            int randomPackSize = Random.Range(0, 100);

            if (randomPackSize < 61)
            {
                packSize = PackSize.Normal;
            }
            else if (randomPackSize < 91)
            {
                packSize = PackSize.Jumbo;
            }
            else
            {
                packSize = PackSize.Mega;
            }


            if (randomPack < 68)
            {
                //CELESTIAL / STANDARD
                int packChoice = Random.Range(0, 3);
                if (packChoice == 0)
                {
                    shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomPlanetPack(packSize));
                }
                else if (packChoice == 1)
                {
                    shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomCardPack(packSize));
                }
                else
                {
                    shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomArcanaPack(packSize));
                }
            }
            else if (randomPack < 90)
            {
                //Buffon
                shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomBuffonPack(packSize));
            }
            else
            {
                shopPack.GetComponent<ShopItem>().SetPackData(DatabaseManager.instance.shopPacksDatabase.GetRandomSpectralPack(packSize));
                
            }
        }
    }

    [ContextMenu("Generate All Shop")]
    private void GenerateAllShop()
    {
        GenerateShopPacks();
        GenerateStuff();
    }
}
