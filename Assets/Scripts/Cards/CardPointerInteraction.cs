using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPointerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float animationTime;
    [SerializeField] private float hoverScale;
    public void OnPointerEnter(PointerEventData data)
    {
        transform.DOScale(hoverScale, animationTime);
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        transform.DOScale(1, animationTime);
    }
}
