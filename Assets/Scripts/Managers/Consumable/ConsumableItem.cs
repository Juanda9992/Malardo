using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public PlanetCardData planetCardData;
    [SerializeField] private TextMeshProUGUI textDescription;

    public void SetPlanetData(PlanetCardData planetCard)
    {
        planetCardData = planetCard;

        PokerHandLevelData pokerHand = PokerHandLevelStorage.instance.GetHandData(planetCardData.handType);

        string fullDesc = pokerHand.pokerHand.name + "\n" + "lvl " + pokerHand.handLevel + "\n" + planetCardData.cardDescription;
        textDescription.text = planetCardData.cardName;
        GetComponent<DescriptionContainer>().SetNameAndDescription(planetCardData.cardName, fullDesc);
    }
    public void UseItem()
    {
        PokerHandUpgrader.instance.RequestUpgradeHand(planetCardData.handType);
    }
}
