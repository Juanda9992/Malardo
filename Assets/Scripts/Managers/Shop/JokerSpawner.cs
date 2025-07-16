using UnityEngine;

public class JokerSpawner : MonoBehaviour
{
    public JokerData[] allJokers;
    [SerializeField] private int defaultJokersAtTime = 2;
    [SerializeField] private Transform jokerTransform;
    [SerializeField] private GameObject jokerPrefab;

    void OnEnable()
    {
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
}
