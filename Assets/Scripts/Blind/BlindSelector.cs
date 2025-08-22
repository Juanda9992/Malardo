using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlindSelector : MonoBehaviour
{
    public UnityEvent OnBlindSelected;
    [SerializeField] private Image bossBlindSprite;
    [SerializeField] private TextMeshProUGUI bossBlindName, bossDescriptionText;
    [SerializeField] private BlindScoreData blindScoreData;
    [SerializeField] private Image[] imageColors;
    [SerializeField] private TextMeshProUGUI[] scoresText;
    [SerializeField] private GameObject[] blindBlockers;
    public CurrentBlind bossBlind;

    void Start()
    {
        GenerateRoundBlinds();
    }

    [ContextMenu("Generate new blind")]
    public void GenerateRoundBlinds()
    {        
        PickUpRandomBossBlind();
        SetUpScoresUI();
        UpdateBlockers();
    }

    private void PickUpRandomBossBlind()
    {
        bossBlind = blindScoreData.bossBlinds[Random.Range(0, blindScoreData.bossBlinds.Length)];

        bossBlindSprite.sprite = bossBlind.blindSprite;
        bossBlindName.text = bossBlind.blindName;
        bossDescriptionText.text = bossBlind.blindDescription;

        Color blindColor = bossBlind.blindColor;
        blindColor *= 0.6f;
        blindColor.a = 1;
        foreach (var bg in imageColors)
        {
            bg.color = blindColor;
        }
    }
    private void SetUpScoresUI()
    {
        int ante = BlindManager.instance.anteLevel;
        for (int i = 0; i < scoresText.Length; i++)
        {
            scoresText[i].text = (blindScoreData.baseScore[ante] * blindScoreData.allBlinds[i].scoreMultiplier).ToString();
        }

        scoresText[2].text = (blindScoreData.baseScore[ante] * bossBlind.blindMultiplier).ToString();
    }

    public void UpdateBlockers()
    {
        foreach (var blocker in blindBlockers)
        {
            blocker.SetActive(true);
        }

        blindBlockers[BlindManager.instance.currentBlindProgress].SetActive(false);
    }

    public void SelectBlind()
    {
        OnBlindSelected?.Invoke();
        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.BlindSelected));
    }

    public CurrentBlind GetCurrentBossBlind()
    {
        return bossBlind;
    }
}
