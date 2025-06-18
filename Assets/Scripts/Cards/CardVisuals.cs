using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upperNumber, lowerNumber;
    private Card _card;

    void Awake()
    {
        transform.DOScale(0, 0);
        transform.DOScale(1, 0.3f);
    }
    public void SetVisuals(Card card)
    {
        _card = card;
        SetLetterOrNumber();
        SetCardColor();
    }

    private void SetLetterOrNumber()
    {
        if (_card.number == 11)
        {
            SetNumberTextValue("J");
        }
        else if (_card.number == 12)
        {
            SetNumberTextValue("Q");
        }
        else if (_card.number == 13)
        {
            SetNumberTextValue("K");
        }
        else
        {
            SetNumberTextValue(_card.number.ToString());
        }
    }
    private void SetVisualsColors(int number)
    {
        upperNumber.color = CardVisualsDatabase.GetInstance().suitsColor[number];
        lowerNumber.color = CardVisualsDatabase.GetInstance().suitsColor[number];
    }
    private void SetNumberTextValue(string value)
    {
        upperNumber.text = value;
        lowerNumber.text = value;
    }

    private void SetCardColor()
    {
        switch (_card.cardSuit)
        {
            case Suit.Hearth:
                SetVisualsColors(0);
                break;
            case Suit.Diamond:
                SetVisualsColors(1);
                break;
            case Suit.Spades:
                SetVisualsColors(2);
                break;
            case Suit.Clover:
                SetVisualsColors(3);
                break;
        }
    }
}
