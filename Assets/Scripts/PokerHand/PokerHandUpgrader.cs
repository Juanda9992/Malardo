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

    public List<HandType> usedCards = new List<HandType>();

    [SerializeField] private HandType upgradeData;

    void Awake()
    {
        instance = this;
    }

    public void RequestUpgradeHand(HandType handType, int levels)
    {
        StartCoroutine(UpgradeVisuals(PokerHandLevelStorage.instance.GetHandData(handType)));

    }

    public void AddCardUsedIntoDatabase(HandType handType)
    {
        if (!usedCards.Contains(handType))
        {
            usedCards.Add(handType);
        }
    }
    public void RequestUpgradeAllHands()
    {
        StartCoroutine(UpgradeVisuals(PokerHandLevelStorage.instance.GetHandData(HandType.High_Card), true, true));
    }

    public void RequestDecreasePokerHand(HandType handType)
    {
        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(handType);

        if (pokerHand.handLevel > 1)
        {
            StartCoroutine(UpgradeVisuals(pokerHand, false));
        }
    }

    public IEnumerator UpgradeVisuals(PokerHandLevelData pokerHandLevelData, bool upgrade = true, bool allHands = false, int levels = 1)
    {
        isUpgrading = true;

        string pokerOperator = upgrade ? "+" : "-";
        string pokerHandName = allHands ? "All Poker Hands" : pokerHandLevelData.pokerHand.name;
        string pokerHandLevel = allHands ? "+1" : pokerHandLevelData.handLevel.ToString();
        string chipsUpgrade = allHands ? "+" : pokerHandLevelData.pokerHand.chipsUpgrade.ToString();
        string multiUPgrade = allHands ? "+" : pokerHandLevelData.pokerHand.multUpgrade.ToString();

        handNameText.text = pokerHandName + " <color=blue> lvl." + pokerHandLevel + "</color>";

        if (!allHands)
        {
            chipUpgrade.upgradeText.text = pokerHandLevelData.GetTotalChips().ToString();
            multUpgrade.upgradeText.text = pokerHandLevelData.GetTotalMult().ToString();
        }
        else
        {
            chipUpgrade.upgradeText.text = "+";
            multUpgrade.upgradeText.text = "+";
        }

        yield return new WaitForSeconds(0.5f);
        chipUpgrade.animationContainer.SetActive(true);
        chipUpgrade.upgradeText.text = pokerOperator + chipsUpgrade;

        yield return new WaitForSeconds(0.4f);
        multUpgrade.animationContainer.SetActive(true);
        multUpgrade.upgradeText.text = pokerOperator + multiUPgrade;

        if (upgrade)
        {
            for (int i = 0; i < levels; i++)
            {
                if (!allHands)
                {
                    pokerHandLevelData.UpgradeHand();
                }
                else
                {
                    foreach (var hand in PokerHandLevelStorage.instance.GetPokerHands())
                    {
                        hand.UpgradeHand();
                    }
                }
            }
        }
        else
        {
            pokerHandLevelData.DecreaseHand();
        }

        handNameText.text = pokerHandName + " <color=blue> lvl." + pokerHandLevel + "</color>";
        yield return new WaitForSeconds(0.6f);

        chipUpgrade.animationContainer.SetActive(false);
        multUpgrade.animationContainer.SetActive(false);
        chipUpgrade.upgradeText.text = allHands ? "+" : pokerHandLevelData.GetTotalChips().ToString();
        multUpgrade.upgradeText.text = allHands ? "+" : pokerHandLevelData.GetTotalMult().ToString();

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
