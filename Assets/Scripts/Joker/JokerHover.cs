using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class JokerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale;
    [SerializeField] private float descriptionYOffset;
    [SerializeField] private JokerContainer jokerContainer;
    public void OnPointerEnter(PointerEventData data)
    {
        transform.DOScale(hoverScale, 0.3f);
        JokerDescription.instance.SetDescriptionOn(jokerContainer._joker, (Vector2)transform.position + new Vector2(0,descriptionYOffset));
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.DOScale(1, 0.2f);
        JokerDescription.instance.SetDescriptionOff();
    }
}
