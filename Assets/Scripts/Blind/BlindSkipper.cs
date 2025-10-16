using TMPro;
using UnityEngine;

public class BlindSkipper : MonoBehaviour
{
    public static BlindSkipper instance;
    [SerializeField] private BlindSelector blindSelector;
    [SerializeField] private TextMeshProUGUI[] skipLabels;
    [SerializeField] private TagGenerator tagGenerator;
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
        tagGenerator.CreateTag(tagGenerator.GetRoundTag(BlindManager.instance.currentBlindProgress));

        skipLabels[BlindManager.instance.currentBlindProgress].gameObject.SetActive(true);
        BlindManager.instance.currentBlindProgress++;
        BlindManager.instance.currentRound++;
        blindSelector.UpdateBlockers();
        BlindManager.instance.UpdateAnteLevelUI();

        GameStatusManager._Status.blindsSkipped++;

        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnBlindSkipped));
        StartCoroutine(tagGenerator.ConsumeTags(TagExchangeMoment.Instant));
    }

    public void TurnOffSkipLabels()
    {
        tagGenerator.GenerateRoundTags();
        foreach (var label in skipLabels)
        {
            label.gameObject.SetActive(false);
        }
    }

}
