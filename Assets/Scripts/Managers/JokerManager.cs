using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private TextMeshProUGUI jokerCounter;

    [SerializeField] private List<JokerExecution> jokers;
    public int JokersInHand { get { return currentJokers.Count; } }
    public static JokerManager instance;

    void Awake()
    {
        instance = this;
        StartCoroutine("UpdateJokerList");
    }

    public IEnumerator PlayJokersAtTime(TriggerEvent eventJokertrigger)
    {
        jokers = new List<JokerExecution>();

        if (eventJokertrigger == TriggerEvent.OnTarotCardUsed)
        {
            ConsumableManager.instance.tarotCardsUsed++;
        }
        for (int i = 0; i < currentJokers.Count; i++)
        {
            foreach (var logics in currentJokers[i]._jokerInstance.jokerLogics)
            {
                if (logics.triggerEvent == eventJokertrigger)
                {
                    jokers.Add(new JokerExecution() { container = currentJokers[i], logic = logics });
                }
            }
        }
        foreach (var joker in jokers)
        {
            if (joker.logic.CanBetriggered())
            {
                joker.container.TriggerActions(joker.logic);
                yield return new WaitWhile(() => PokerHandUpgrader.instance.isUpgrading == true);
                yield return new WaitForSeconds(0.3f);
            }
            if (joker.container._jokerInstance.destroyJoker)
            {
                joker.container._jokerInstance.triggerMessage = "BYE!";
                joker.container.TriggerMessage();
                yield return new WaitForSeconds(0.2f);
                RemoveJoker(joker.container);
            }
        }

        if (eventJokertrigger == TriggerEvent.OnHandEnd)
        {
            for (int i = 0; i < currentJokers.Count; i++)
            {
                if (currentJokers[i]._jokerInstance.jokerEdition != CardEdition.Base)
                {
                    yield return PlayJokerEdition(currentJokers[i]._jokerInstance);
                }
            }
        }

    }

    private IEnumerator PlayJokerEdition(JokerInstance jokerInstance)
    {
        if (jokerInstance.jokerEdition == CardEdition.Foil)
        {
            ScoreManager.instance.AddChips(50);
            jokerInstance.jokerContainer.TriggerMessage("+50");
        }
        else if (jokerInstance.jokerEdition == CardEdition.Holographic)
        {
            ScoreManager.instance.AddMult(10);
            jokerInstance.jokerContainer.TriggerMessage("+10");
        }
        else if (jokerInstance.jokerEdition == CardEdition.Polychrome)
        {
            ScoreManager.instance.MultiplyMulti(1.5f);
            jokerInstance.jokerContainer.TriggerMessage("x1.5");
        }
        yield return new WaitForSeconds(0.2f);
    }
    public void AddJoker(JokerData jokerData, bool isNegative)
    {
        JokerContainer createdJoker = CreateJoker(jokerData);
        GameStatusManager.SetJokersInMatch(currentJokers.Count);
        currentJokers.Add(createdJoker.GetComponent<JokerContainer>());

        if (isNegative)
        {
            maximumJokers++;
        }


        jokerCounter.text = currentJokers.Count + "/" + maximumJokers;
    }

    public void AddJoker(JokerInstance jokerInstance)
    {
        JokerContainer createdJoker = CreateJoker(jokerInstance.data);

        createdJoker._jokerInstance.SetInstanceData(jokerInstance);
        GameStatusManager.SetJokersInMatch(currentJokers.Count);

        foreach (var logic in createdJoker._jokerInstance.jokerLogics)
        {
            logic.jokerEffect[0].UpdateDescription(createdJoker._jokerInstance);
        }

        if (jokerInstance.jokerEdition == CardEdition.Negative)
        {
            maximumJokers++;
        }
        currentJokers.Add(createdJoker.GetComponent<JokerContainer>());
        createdJoker.GetComponent<JokerContainer>().SetJokerEdition(jokerInstance.jokerEdition);

        jokerCounter.text = currentJokers.Count + "/" + maximumJokers;
    }

    private JokerContainer CreateJoker(JokerData jokerData)
    {
        GameObject newJoker = Instantiate(jokerCard, jokerParent);

        JokerContainer jokerContainer = newJoker.GetComponent<JokerContainer>();

        JokerInstance jokerInstance = new JokerInstance(jokerData);
        jokerContainer.SetUpJoker(jokerInstance);
        jokerContainer.isOnShop = false;

        if (jokerInstance.jokerEdition == CardEdition.Negative)
        {
            maximumJokers++;
        }

        for (int i = 0; i < jokerContainer._jokerInstance.data.OnSetUpJoker.Count; i++)
        {
            jokerContainer._jokerInstance.data.OnSetUpJoker[i].ApplyEffect(jokerInstance);
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
        for (int i = 0; i < jokerContainer._jokerInstance.data.OnSellEffect.Count; i++)
        {
            jokerContainer._jokerInstance.data.OnSellEffect[i].ApplyEffect(jokerContainer._jokerInstance);
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
        return currentJokers.FindAll(x => x._jokerInstance.data.jokerRarity == rarity).Count;
    }

    public int GetSellValueFromAllJokers()
    {
        int ammount = 0;
        foreach (var joker in currentJokers)
        {
            ammount += joker._jokerInstance.sellValue;
        }
        return ammount;
    }
    public void SetCopyRandomJokerCoroutine()
    {
        StartCoroutine("CopyRandomJoker");
    }

    public IEnumerator CopyRandomJoker()
    {
        JokerInstance copyJoker = currentJokers[Random.Range(0, currentJokers.Count)]._jokerInstance;

        ClearAllJokers();

        yield return new WaitForSeconds(0.2f);

        AddJoker(copyJoker);
        AddJoker(copyJoker);
    }
    public IEnumerator AddEditionToRandomJoker(CardEdition jokerEdition, bool destroyOthers)
    {

        List<JokerContainer> deleteJoker = new List<JokerContainer>();
        JokerContainer copyJoker = currentJokers[Random.Range(0, currentJokers.Count)];

        for (int i = 0; i < currentJokers.Count; i++)
        {
            if (!GameObject.ReferenceEquals(copyJoker.gameObject, currentJokers[i].gameObject))
            {
                deleteJoker.Add(currentJokers[i]);
            }
        }

        foreach (var joker in deleteJoker)
        {
            RemoveJoker(joker);
            yield return new WaitForSeconds(0.2f);
        }

        copyJoker.SetJokerEdition(jokerEdition);
    }

    public void ReorderJokers()
    {
        currentJokers = currentJokers.OrderBy(x => x.transform.position.x).ToList();

        for (int i = 0; i < currentJokers.Count; i++)
        {
            currentJokers[i].transform.SetSiblingIndex(i);
        }
        jokerParent.gameObject.SetActive(false);
        jokerParent.gameObject.SetActive(true);
    }

    private void ClearAllJokers()
    {
        foreach (var joker in currentJokers)
        {
            Destroy(joker.gameObject);
        }

        currentJokers.Clear();
        jokerCounter.text = "0/" + maximumJokers;
    }

    [ContextMenu("Test Add joker")]
    private void TestJoker()
    {
        JokerInstance jokerInstance = new JokerInstance(testjoker);
        AddJoker(jokerInstance);
    }
}

[System.Serializable]
public class JokerExecution
{
    public JokerContainer container;
    public JokerLogic logic;
}