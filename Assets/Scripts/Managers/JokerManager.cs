using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public List<JokerData> currentJokers = new List<JokerData>();
    public int maximumJokers = 5;
    [SerializeField] private GameObject jokerCard;
    [SerializeField] private Transform jokerParent;

    [SerializeField] private JokerData testjoker;

    public int JokersInHand {get{ return currentJokers.Count; }}
    public static JokerManager instance;

    void Awake()
    {
        instance = this;
    }
    public void AddJoker(JokerData jokerData)
    {
        currentJokers.Add(jokerData);

        GameObject newJoker = Instantiate(jokerCard, jokerParent);

        newJoker.GetComponent<JokerContainer>().SetUpJoker(jokerData);

        GameStatusManager.SetJokersInMatch(currentJokers.Count);
    }


    [ContextMenu("Test Add joker")]
    private void TestJoker()
    {
        AddJoker(testjoker);
    }
}
