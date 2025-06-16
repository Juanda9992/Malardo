using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upperNumber, lowerNumber;

    public void SetVisuals(Card card)
    {
        SetLetterOrNumber(card);
    }

    private void SetLetterOrNumber(Card card)
    {
        if (card.number == 11)
        {
            SetNumberTextValue("J");
        }
        else if (card.number == 12)
        {
            SetNumberTextValue("Q");
        }
        else if (card.number == 13)
        {
            SetNumberTextValue("K");
        }
        else
        {
            SetNumberTextValue(card.number.ToString());
        }
    }

    private void SetNumberTextValue(string value)
    {
        upperNumber.text = value;
        lowerNumber.text = value;
    }
}
