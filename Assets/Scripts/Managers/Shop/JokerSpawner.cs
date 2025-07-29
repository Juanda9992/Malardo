using System.Collections;
using UnityEngine;

public class JokerSpawner : MonoBehaviour
{
    public JokerData[] allJokers;
    [SerializeField] private int defaultJokersAtTime = 2;

    [Header("Joker Spawning")]
    [SerializeField] private Transform jokerTransform;
    [SerializeField] private GameObject jokerPrefab;

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
        GenerateJokers();
    }
    public void GenerateJokers()
    {
        Transform[] existingJokers = jokerTransform.GetComponentsInChildren<Transform>();
        if (existingJokers.Length > 1)
        {
            for (int i = 1; i < existingJokers.Length; i++)
            {
                Destroy(existingJokers[i].gameObject);
            }
        }
        for (int i = 0; i < defaultJokersAtTime; i++)
        {
            GameObject currentJoker = Instantiate(jokerPrefab, jokerTransform);
            currentJoker.GetComponent<JokerContainer>().SetUpJoker(allJokers[Random.Range(0, allJokers.Length)]);
        }

        gameObject.SetActive(false);
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
}
