using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BlindSkipper : MonoBehaviour
{
    public static BlindSkipper instance;
    [SerializeField] private BlindSelector blindSelector;
    [SerializeField] private TextMeshProUGUI[] skipLabels;
    [SerializeField] private TagBehaviour[] tagBehaviour;
    [SerializeField] private GameObject tagPrefab;
    [SerializeField] private Transform tagParent;

    [SerializeField] private List<TagBehaviour> currentTags = new List<TagBehaviour>();

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentTags = new List<TagBehaviour>();
        TurnOffSkipLabels();
    }

    [ContextMenu("Skip Blind")]
    public void SkipBlind()
    {
        CreateTag();

        skipLabels[BlindManager.instance.currentBlindProgress].gameObject.SetActive(true);
        BlindManager.instance.currentBlindProgress++;
        BlindManager.instance.currentRound++;
        blindSelector.UpdateBlockers();
        BlindManager.instance.UpdateAnteLevelUI();

        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnBlindSkipped));
        StartCoroutine(ConsumeTags(TagExchangeMoment.Instant));
    }

    public void TurnOffSkipLabels()
    {
        GenerateRoundTags();
        foreach (var label in skipLabels)
        {
            label.gameObject.SetActive(false);
        }
    }
    private void CreateTag()
    {
        GameObject go = Instantiate(tagPrefab, tagParent);

        go.GetComponent<TagBehaviour>().SetTagData(tagBehaviour[BlindManager.instance.currentBlindProgress].GetCurrentTag());

        currentTags.Add(go.GetComponent<TagBehaviour>());
    }

    private void GenerateRoundTags()
    {
        for (int i = 0; i < tagBehaviour.Length; i++)
        {
            tagBehaviour[i].SetTagData(DatabaseManager.instance.tagDatabase.GetRandomTag());
        }
    }

    public IEnumerator ConsumeTags(TagExchangeMoment tagExchangeMoment)
    {
        for (int i = 0; i < currentTags.Count; i++)
        {
            if (currentTags[i].GetCurrentTag().tagExchangeMoment == tagExchangeMoment)
            {
                yield return new WaitForSeconds(0.5f);
                currentTags[i].GetCurrentTag().tagEffect.ApplyEffect();
                Destroy(currentTags[i].gameObject);
                currentTags[i] = null;
            }
            else
            {
                break;
            }
        }

        currentTags.RemoveAll(x => x == null);
    }
}
