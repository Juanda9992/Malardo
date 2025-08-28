using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PokerHandUpgrader : MonoBehaviour
{
    public bool isUpgrading;
    public static PokerHandUpgrader instance;
    public TextMeshProUGUI handNameText;
    public StatUpgrade multUpgrade;
    public StatUpgrade chipUpgrade;

    [SerializeField] private HandType upgradeData;

    void Awake()
    {
        instance = this;
    }

    public void RequestUpgradeHand(HandType handType)
    {
        StartCoroutine(UpgradeVisuals(PokerHandLevelStorage.instance.GetHandData(handType)));
    }

    public void RequestDecreasePokerHand(HandType handType)
    {
        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(handType);

        if (pokerHand.handLevel > 1)
        {
            StartCoroutine(UpgradeVisuals(pokerHand, false));
        }
    }

    public IEnumerator UpgradeVisuals(PokerHandLevelData pokerHandLevelData,bool upgrade = true)
    {
        isUpgrading = true;

        string pokerOperator = upgrade ? "+" : "-"; 

        handNameText.text = pokerHandLevelData.pokerHand.name + " <color=blue> lvl." + pokerHandLevelData.handLevel + "</color>";

        chipUpgrade.upgradeText.text = pokerHandLevelData.GetTotalChips().ToString();
        multUpgrade.upgradeText.text = pokerHandLevelData.GetTotalMult().ToString();

        yield return new WaitForSeconds(0.5f);
        chipUpgrade.animationContainer.SetActive(true);
        chipUpgrade.upgradeText.text = pokerOperator + pokerHandLevelData.pokerHand.chipsUpgrade;

        yield return new WaitForSeconds(0.4f);
        multUpgrade.animationContainer.SetActive(true);
        multUpgrade.upgradeText.text = pokerOperator + pokerHandLevelData.pokerHand.multUpgrade;

        if (upgrade)
        {
            pokerHandLevelData.UpgradeHand();
        }
        else
        {
            pokerHandLevelData.DecreaseHand();
        }

        handNameText.text = pokerHandLevelData.pokerHand.name + " <color=blue> lvl." + pokerHandLevelData.handLevel + "</color>";
        yield return new WaitForSeconds(0.6f);

        chipUpgrade.animationContainer.SetActive(false);
        multUpgrade.animationContainer.SetActive(false);
        chipUpgrade.upgradeText.text = pokerHandLevelData.GetTotalChips().ToString();
        multUpgrade.upgradeText.text = pokerHandLevelData.GetTotalMult().ToString();

        yield return new WaitForSeconds(0.3f);
        chipUpgrade.upgradeText.text = "0";
        multUpgrade.upgradeText.text = "0";
        handNameText.text = "";
        if (CardPlayer.instance.isPlayingCards)
        {
            handNameText.text = pokerHandLevelData.pokerHand.name + " <color=blue> lvl." + pokerHandLevelData.handLevel + "</color>";
            ScoreManager.instance.SetChips(pokerHandLevelData.GetTotalChips());
            ScoreManager.instance.SetMult(pokerHandLevelData.GetTotalMult());
            yield return new WaitForSeconds(0.2f);
        }
        isUpgrading = false;
    }

    [ContextMenu("Test upgrade UI")]
    private void TestUpgrade()
    {
        StartCoroutine(UpgradeVisuals(PokerHandLevelStorage.instance.GetHandData(upgradeData)));
    }
}

[System.Serializable]
public class StatUpgrade
{
    public GameObject animationContainer;
    public TextMeshProUGUI upgradeText;
}
