using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public List<JokerData> currentJokers = new List<JokerData>();

    [SerializeField] private GameObject jokerCard;
    [SerializeField] private Transform jokerParent;

    [SerializeField] private JokerData testjoker;
    public void AddJoker(JokerData jokerData)
    {
        currentJokers.Add(jokerData);

        GameObject newJoker = Instantiate(jokerCard, jokerParent);

        newJoker.GetComponent<JokerContainer>().SetUpJoker(jokerData);
    }


    [ContextMenu("Test Add joker")]
    private void TestJoker()
    {
        AddJoker(testjoker);
    }
}
