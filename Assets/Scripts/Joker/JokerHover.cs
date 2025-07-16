using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
public class JokerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float hoverScale;
    [SerializeField] private float descriptionXOffset;
    [SerializeField] private JokerContainer jokerContainer;
    [SerializeField] private UnityEvent OnClicked;
    public void OnPointerEnter(PointerEventData data)
    {
        transform.DOScale(hoverScale, 0.3f);
        JokerDescription.instance.SetDescriptionOn(jokerContainer._joker, (Vector2)transform.position + new Vector2(descriptionXOffset,0));
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.DOScale(1, 0.2f);
        JokerDescription.instance.SetDescriptionOff();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke();
    }
}
