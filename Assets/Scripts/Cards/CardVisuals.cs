using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] private Image bgImage,numbersImage;
    [SerializeField] private int rotationRange;

    [SerializeField] private GameObject[] editionsContainer;
    [SerializeField] private GameObject bonusCardVisuals, multCardVisuals,wildCardVisuals, disabledCardVisuals;
    [SerializeField] private Image seal;
    [SerializeField] private Suit testSuit;
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
        _card.SetCardName();
        _card.SetCardChipAmmount();
        numbersImage.sprite = DatabaseManager.instance.cardSpriteDatabase.GetCardSprite(_card.cardSuit, _card.number == 14 ? 1:_card.number);
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
        seal.gameObject.SetActive(true);
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
        numbersImage.color = Color.white;
        bonusCardVisuals.SetActive(false);
        multCardVisuals.SetActive(false);
        wildCardVisuals.SetActive(false);

        if (_card.cardType == CardType.Gold)
        {
            bgImage.color = DatabaseManager.instance.cardColorDatabase.goldCard;
        }

        else if (_card.cardType == CardType.Silver)
        {
            bgImage.sprite = DatabaseManager.instance.cardSpriteDatabase.silverCard;
        }
        else if (_card.cardType == CardType.Lucky)
        {
            bgImage.color = DatabaseManager.instance.cardColorDatabase.luckyCard;
        }
        else if (_card.cardType == CardType.Stone)
        {
            numbersImage.gameObject.SetActive(false);
            bgImage.color = DatabaseManager.instance.cardColorDatabase.stoneCard;
        }
        else if (_card.cardType == CardType.Glass)
        {
            bgImage.sprite = DatabaseManager.instance.cardSpriteDatabase.glassCard;
        }
        else if (_card.cardType == CardType.Bonus)
        {
            bonusCardVisuals.SetActive(true);
        }
        else if (_card.cardType == CardType.Mult)
        {
            multCardVisuals.SetActive(true);
        }
        else if (_card.cardType == CardType.Wild)
        {
            wildCardVisuals.SetActive(true);
        }
    }
    public void SetCardDisabled(bool disabled)
    {
        disabledCardVisuals.SetActive(disabled);
    }

    public void UpdateCardSuitCoroutineRequest(Suit suit)
    {
        StartCoroutine(FlipCard(() => UpdateCardSuit(suit)));
    }

    public void UpdateCardTypeCoroutineRequest(CardType cardType)
    {
        StartCoroutine(FlipCard(() => UpdateCardType(cardType)));
    }

    public void CoptyCardCoroutineRequest(Card card)
    {
        StartCoroutine(FlipCard(() => CopyCard(card)));
    }

    public void IncreaseCardRankCoroutineRequest()
    {
        StartCoroutine(FlipCard(() => IncreaseRank()));
    }

    public void UpdateNumberCoroutine(int number)
    {
        StartCoroutine(FlipCard(() => UpdateNumber(number)));
    }

    private void IncreaseRank()
    {
        _card.number++;

        _card.number = _card.number == 14 ? 1 : _card.number;
        SetVisuals(_card);
    }

    private void CopyCard(Card card)
    {
        _card.CopyCardData(card);
        SetVisuals(_card);
    }

    private void UpdateNumber(int number)
    {
        _card.number = number;
        SetVisuals(_card);
    }
    

    private void UpdateCardType(CardType cardType)
    {
        _card.cardType = cardType;
        SetVisuals(_card);
    }
    private void UpdateCardSuit(Suit suit)
    {
        _card.cardSuit = suit;
        SetVisuals(_card);
    }

    [ContextMenu("Test Card FLip")]
    private void AnimateCard()
    {
        StartCoroutine(FlipCard(()=>UpdateCardSuitCoroutineRequest(testSuit)));
    }

    private IEnumerator FlipCard(Action action)
    {
        yield return transform.DOLocalRotate(Vector3.up * 90, 0.2f);
        yield return new WaitForSeconds(0.2f);
        action?.Invoke();
        yield return new WaitForSeconds(0.2f);
        yield return transform.DOLocalRotate(Vector3.up * 0, 0.2f);
    }

}
