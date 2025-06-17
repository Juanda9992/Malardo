using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPointerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float animationTime;
    [SerializeField] private float hoverScale;
    [SerializeField] private float moveYOffset;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float initialY;

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
        selected = true;
        transform.DOLocalMoveY(transform.localPosition.y + moveYOffset, animationTime);
        
    }

    public void UnSelect()
    {
        selected = false;
        transform.DOLocalMoveY(initialY, animationTime);
    }
    

}
