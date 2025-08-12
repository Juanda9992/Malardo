using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PokerHandUpgrader : MonoBehaviour
{
    public TextMeshProUGUI handNameText;
    public StatUpgrade multUpgrade;
    public StatUpgrade chipUpgrade;

    [SerializeField] private HandType upgradeData;
    public IEnumerator UpgradeVisuals(PokerHandLevelData pokerHandLevelData)
    {
        handNameText.text = pokerHandLevelData.pokerHand.name + " <color=blue> lvl." + pokerHandLevelData.handLevel + "</color>";

        chipUpgrade.upgradeText.text = pokerHandLevelData.GetTotalChips().ToString();
        multUpgrade.upgradeText.text = pokerHandLevelData.GetTotalMult().ToString();

        yield return new WaitForSeconds(0.5f);
        chipUpgrade.animationContainer.SetActive(true);
        chipUpgrade.upgradeText.text = "+" + pokerHandLevelData.pokerHand.chipsUpgrade;

        yield return new WaitForSeconds(0.4f);
        multUpgrade.animationContainer.SetActive(true);
        multUpgrade.upgradeText.text = "+" + pokerHandLevelData.pokerHand.multUpgrade;

        pokerHandLevelData.UpgradeHand();

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
