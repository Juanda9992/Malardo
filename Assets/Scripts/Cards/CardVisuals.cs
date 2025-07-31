using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private int rotationRange;

    [SerializeField] private GameObject[] editionsContainer;
    [SerializeField] private GameObject bonusCardVisuals, multCardVisuals;
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
        cardImage.sprite = DatabaseManager.instance.cardSpriteDatabase.GetCardSprite(_card.cardSuit, _card.number);
        SetCardEdition();
        SetCardSeal();
        SetCardTypeVisuals();
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

    private void SetCardTypeVisuals()
    {
        if (_card.cardType == CardType.Gold)
        {
            cardImage.color = DatabaseManager.instance.cardColorDatabase.goldCard;
        }

        else if (_card.cardType == CardType.Silver)
        {
            cardImage.color = DatabaseManager.instance.cardColorDatabase.steelCard;
        }
        else if (_card.cardType == CardType.Lucky)
        {
            cardImage.color = DatabaseManager.instance.cardColorDatabase.luckyCard;
        }
        else if (_card.cardType == CardType.Stone)
        {
            cardImage.color = DatabaseManager.instance.cardColorDatabase.stoneCard;
        }
        else if (_card.cardType == CardType.Glass)
        {
            cardImage.color = DatabaseManager.instance.cardColorDatabase.glassCard;
        }
        else if (_card.cardType == CardType.Bonus)
        {
            bonusCardVisuals.SetActive(true);
        }
        else if (_card.cardType == CardType.Mult)
        {
            multCardVisuals.SetActive(true);
        }
    }

}
