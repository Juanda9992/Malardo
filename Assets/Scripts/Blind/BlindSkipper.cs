using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
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
    }

    private void GenerateRoundTags()
    {
        for (int i = 0; i < tagBehaviour.Length; i++)
        {
            tagBehaviour[i].SetTagData(DatabaseManager.instance.tagDatabase.GetRandomTag());
        }
    }
}
