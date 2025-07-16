using UnityEngine;
using UnityEngine.EventSystems;

public class JokerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float yOffset;
    [SerializeField] private JokerContainer jokerContainer;
    public void OnPointerEnter(PointerEventData data)
    {
        JokerDescription.instance.SetDescriptionOn(jokerContainer._joker, (Vector2)transform.position + new Vector2(0,yOffset));
    }

    public void OnPointerExit(PointerEventData data)
    {
        JokerDescription.instance.SetDescriptionOff();
    }
}
