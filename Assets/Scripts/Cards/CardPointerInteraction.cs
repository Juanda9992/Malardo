using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class CardPointerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Card_Data card_Data;

    [SerializeField] private float animationTime;
    [SerializeField] private float hoverScale;
    [SerializeField] private float moveYOffset;
    [SerializeField] private RectTransform _rectTransform;
    private float initialY;

    private bool selected = false;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        initialY = _rectTransform.localPosition.y;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        transform.DOScale(hoverScale, animationTime);
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.DOScale(1, animationTime);
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (selected)
        {
            UnSelect();
        }
        else
        {
            Select();
        }
    }

    public void Select()
    {
        if (HandManager.instance.CanAddCards)
        {
            selected = true;
            transform.DOLocalMoveY(transform.localPosition.y + moveYOffset, animationTime);
            HandManager.instance.AddCardToHand(card_Data.currentCard);
        }

    }

    public void UnSelect()
    {
        selected = false;
        transform.DOLocalMoveY(initialY, animationTime);
        HandManager.instance.RemoveCardFromHand(card_Data.currentCard);
    }

    public void DiscardCard()
    {
        transform.DOScale(0, 0.2f).OnComplete(() =>
        {
            CardManager.instance.GenerateCardOnHand();
            CardManager.instance.RemoveCardFromDeck(card_Data.currentCard);
            Destroy(gameObject);
        });
    }

}
