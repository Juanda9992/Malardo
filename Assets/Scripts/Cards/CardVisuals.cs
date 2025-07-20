using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upperNumber, lowerNumber;
    [SerializeField] private Image cardImage;
    [SerializeField] private int rotationRange;

    [SerializeField] private GameObject[] editionsContainer;
    [SerializeField] private Image seal;
    private Card _card;

    void Awake()
    {
        transform.DOScale(0, 0);
        transform.DOScale(1, 0.3f);

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-rotationRange, rotationRange));
    }
    public void SetVisuals(Card card)
    {
        _card = card;
        SetLetterOrNumber();
        SetCardColor();
        SetCardEdition();
        SetCardSeal();
    }

    private void SetCardEdition()
    {
        if (_card.cardEdition == CardEdition.Foil)
        {
            editionsContainer[0].SetActive(true);
        }
        else if (_card.cardEdition == CardEdition.Holographic)
        {
            editionsContainer[1].SetActive(true);
        }
        else if (_card.cardEdition == CardEdition.Polychrome)
        {
            editionsContainer[2].SetActive(true);
        }
    }

    private void SetCardSeal()
    {
        if (_card.cardSeal == Seal.Gold)
        {
            seal.color = Color.yellow;
        }
        else if (_card.cardSeal == Seal.Red)
        {
            seal.color = Color.red;
        }
        else if (_card.cardSeal == Seal.Blue)
        {
            seal.color = Color.blue;
        }
        else if (_card.cardSeal == Seal.Purple)
        {
            seal.color = Color.magenta;
        }
        else
        {
            seal.gameObject.SetActive(false);
        }


    }

    private void SetLetterOrNumber()
    {

        if (_card.cardType == CardType.Gold)
        {
            cardImage.color = Color.yellow;
        }
        if (_card.cardType == CardType.Stone)
        {
            if (_card.cardType == CardType.Stone)
            {
                cardImage.color = Color.grey;
            }
            upperNumber.gameObject.SetActive(false);
            lowerNumber.gameObject.SetActive(false);
        }
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
