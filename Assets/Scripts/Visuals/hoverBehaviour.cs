using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class hoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float hoverScaleFactor;

    [SerializeField] private UnityEvent OnHoverIn, OnHoverOut, OnClicked;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(hoverScaleFactor, 0.2f);
        OnHoverIn?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.2f);
        OnHoverOut?.Invoke();
    }
    public void OnPointerClick(PointerEventData data)
    {
        OnClicked?.Invoke();
    }
}
