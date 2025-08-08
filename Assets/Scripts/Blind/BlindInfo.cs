using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlindInfo : MonoBehaviour
{
    [SerializeField] private CurrentBlind bossBlind;

    [SerializeField] private TextMeshProUGUI bossBlindName, bossBLindEffect;
    [SerializeField] private Image bossBlindImage, bossLabelBg;

    [SerializeField] private TextMeshProUGUI[] blindStatus;
    [SerializeField] private TextMeshProUGUI[] blindScoreRequirements;
    [SerializeField] private Image[] blindStatusBgs;
    [SerializeField] private Color upcomingColor, currentColor;
    [SerializeField] private BlindScoreData blindScoreData;
    public void SetBossBlind()
    {
        bossBlind = GameObject.FindObjectOfType<BlindSelector>().GetCurrentBossBlind();
        bossBlindName.text = bossBlind.blindName;
        bossBLindEffect.text = bossBlind.blindDescription;
        bossBlindImage.sprite = bossBlind.blindSprite;


        Color bossColor = bossBlind.blindColor * 0.5f;
        bossColor.a = 1;
        bossLabelBg.color = bossColor;

        for (int i = 0; i < blindStatus.Length; i++)
        {
            blindStatus[i].text = "Upcoming";
            blindStatusBgs[i].color = upcomingColor;
        }

        blindStatus[BlindManager.instance.currentBlindProgress].text = "Current";
        blindStatusBgs[BlindManager.instance.currentBlindProgress].color = currentColor;
        SetUpScoresUI();
    }

    private void SetUpScoresUI()
    {
        int ante = BlindManager.instance.anteLevel;
        for (int i = 0; i < blindScoreRequirements.Length; i++)
        {
            blindScoreRequirements[i].text = (blindScoreData.baseScore[ante] * blindScoreData.allBlinds[i].scoreMultiplier).ToString();
        }

        blindScoreRequirements[2].text = (blindScoreData.baseScore[ante] * bossBlind.blindMultiplier).ToString();
    }
}
