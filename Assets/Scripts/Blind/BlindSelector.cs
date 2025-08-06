using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlindSelector : MonoBehaviour
{
    [SerializeField] private Image bossBlindSprite;
    [SerializeField] private TextMeshProUGUI bossBlindName, bossDescriptionText;
    [SerializeField] private BlindScoreData blindScoreData;
    [SerializeField] private Image[] imageColors;
    private CurrentBlind bossBlind;

    void Start()
    {
        PickUpRandomBossBlind();
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
}
