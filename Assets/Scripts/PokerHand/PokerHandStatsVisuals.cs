using TMPro;
using UnityEngine;

public class PokerHandStatsVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI pokerHandNameText;
    [SerializeField] private TextMeshProUGUI chipAmmountText, multAmmountext;
    [SerializeField] private TextMeshProUGUI playedTimesText;


    public void SetUpData(PokerHandLevelData pokerHandLevel)
    {
        levelText.text = "lvl." + pokerHandLevel.handLevel;
        pokerHandNameText.text = pokerHandLevel.pokerHand.name;
        chipAmmountText.text = pokerHandLevel.GetTotalChips().ToString();
        multAmmountext.text = pokerHandLevel.GetTotalMult().ToString();
        playedTimesText.text = "0";
    }
}
