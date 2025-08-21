using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    public List<JokerContainer> currentJokers = new List<JokerContainer>();
    [SerializeField] private List<JokerData> testingJokers;
    public int maximumJokers = 5;
    [SerializeField] private GameObject jokerCard;
    [SerializeField] private Transform jokerParent;

    [SerializeField] private JokerData testjoker;

    [SerializeField] private JokerTrigger endHandTrigger;
    [SerializeField] private TextMeshProUGUI jokerCounter;

    public int JokersInHand { get { return currentJokers.Count; } }
    public static JokerManager instance;

    void Awake()
    {
        instance = this;
        StartCoroutine("UpdateJokerList");
    }

    public IEnumerator PlayJokersEndMatch()
    {
        List<JokerContainer> endJokers = new List<JokerContainer>();

        for (int i = 0; i < currentJokers.Count; i++)
        {
            if (currentJokers[i]._joker.triggers.Contains(endHandTrigger))
            {
                endJokers.Add(currentJokers[i]);
            }
        }
        for (int i = 0; i < endJokers.Count; i++)
        {
            if (endJokers[i].CanBeTriggered())
            {
                endJokers[i].TriggerActions();
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(0.2f);
    }
    public void AddJoker(JokerData jokerData)
    {
        JokerContainer createdJoker = CreateJoker(jokerData);
        GameStatusManager.SetJokersInMatch(currentJokers.Count);
        currentJokers.Add(createdJoker.GetComponent<JokerContainer>());

        
        jokerCounter.text = currentJokers.Count + "/" + maximumJokers;
    }

    private JokerContainer CreateJoker(JokerData jokerData)
    {
        GameObject newJoker = Instantiate(jokerCard, jokerParent);

        JokerContainer jokerContainer = newJoker.GetComponent<JokerContainer>();

        JokerInstance jokerInstance = new JokerInstance(jokerData);
        jokerContainer.SetUpJoker(jokerInstance);
        jokerContainer.isOnShop = false;

        for (int i = 0; i < jokerContainer._joker.OnSetUpJoker.Count; i++)
        {
            jokerContainer._joker.OnSetUpJoker[i].ApplyEffect();
        }
        return jokerContainer;
    }

    public IEnumerator UpdateJokerList()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < testingJokers.Count; i++)
        {
            currentJokers.Add(CreateJoker(testingJokers[i]));
        }
        jokerCounter.text = currentJokers.Count + "/" + maximumJokers;
    }
    public void RemoveJoker(JokerContainer jokerContainer)
    {
        for (int i = 0; i < jokerContainer._joker.OnSellEffect.Count; i++)
        {
            jokerContainer._joker.OnSellEffect[i].ApplyEffect();
        }
        currentJokers.Remove(jokerContainer);
        Destroy(jokerContainer.gameObject);

        jokerCounter.text = currentJokers.Count + "/" + maximumJokers;
    }

    public bool CanAddJoker()
    {
        return currentJokers.Count < maximumJokers;
    }

    public int GetCurrentJokersByRarity(JokerRarity rarity)
    {
        return currentJokers.FindAll(x => x._joker.jokerRarity == rarity).Count;
    }

    public int GetSellValueFromAllJokers()
    {
        int ammount = 0;
        foreach (var joker in currentJokers)
        {
            ammount += joker._joker.sellValue;
        }
        return ammount;
    }


    [ContextMenu("Test Add joker")]
    private void TestJoker()
    {
        AddJoker(testjoker);
    }
}
