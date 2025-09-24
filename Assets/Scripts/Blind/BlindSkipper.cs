using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlindSkipper : MonoBehaviour
{
    public static BlindSkipper instance;
    [SerializeField] private BlindSelector blindSelector;
    [SerializeField] private TextMeshProUGUI[] skipLabels;

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
        skipLabels[BlindManager.instance.currentBlindProgress].gameObject.SetActive(true);
        BlindManager.instance.currentBlindProgress++;
        BlindManager.instance.currentRound++;
        blindSelector.UpdateBlockers();
        BlindManager.instance.UpdateAnteLevelUI();
    }

    public void TurnOffSkipLabels()
    {
        foreach (var label in skipLabels)
        {
            label.gameObject.SetActive(false);
        }
    }
}
