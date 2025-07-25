using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class CardPointerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private InputActionReference mousePos;
    [SerializeField] private Card_Data card_Data;

    [SerializeField] private float animationTime;
    [SerializeField] private float hoverScale;
    [SerializeField] private float moveYOffset;
    [SerializeField] private RectTransform _rectTransform;
    private float initialY;

    private bool selected = false;
    [SerializeField] private bool grabbed = false;
    private Vector2 previousPos;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        initialY = _rectTransform.localPosition.y;
        mousePos.action.Enable();
    }
    public void OnPointerEnter(PointerEventData data)
    {
        transform.DOScale(hoverScale, animationTime);
        JokerDescription.instance.SetCardDescription(card_Data);
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.DOScale(1, animationTime);
        JokerDescription.instance.SetDescriptionOff();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (grabbed || CardPlayer.instance.isPlayingCards)
        {
            return;
        }
        if (data.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (selected)
        {
            UnSelect();
        }
        else
        {

            Select();
        }
    }

    #region Drag and Drop
    public void OnPointerUp(PointerEventData data)
    {
        StopCoroutine(nameof(StartDragging));
        if (!grabbed)
        {
            return;
        }
        CardsReorder.instance.ReorderCards();
        if (!grabbed)
        {
            return;
        }
        StartCoroutine(nameof(StopDragging));
    }

    public void OnPointerDown(PointerEventData data)
    {   
        if (data.button != PointerEventData.InputButton.Left || CardPlayer.instance.isPlayingCards)
        {
            return;
        }
        StartCoroutine(nameof(StartDragging));
    }

    private IEnumerator StartDragging()
    {
        yield return new WaitForSeconds(0.15f);
        CardsReorder.instance.DisableLayout();
        transform.SetSiblingIndex(transform.parent.childCount);
        previousPos = transform.localPosition;
        grabbed = true;
    }

    private IEnumerator StopDragging()
    {
        yield return new WaitForEndOfFrame();
        grabbed = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        HandManager.instance.UpdateHandCardsPosition();

    }

    public void UpdateCardPos()
    {
        if (selected)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, initialY + moveYOffset);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x, initialY);
        }
    }

    void Update()
    {
        if (grabbed)
        {
            transform.position = mousePos.action.ReadValue<Vector2>();
        }
    }
    #endregion
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
        previousPos = new Vector2(transform.localPosition.x, initialY);
        HandManager.instance.RemoveCardFromHand(card_Data.currentCard);
    }
    public void DestroyCard()
    {
        DeckManager.instance.RemoveCardFromDeck(card_Data.currentCard);
        transform.DOScale(0, 0.2f).OnComplete(() => Destroy(gameObject));
    }

    public void ShakeCard()
    {
        transform.DOPunchScale(Vector3.one * 0.3f, 0.15f);
    }

}
