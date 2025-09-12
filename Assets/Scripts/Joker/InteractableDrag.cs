using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractableDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public UnityEvent OnDropEvent;
    [SerializeField] private InputActionReference inputAction;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = inputAction.action.ReadValue<Vector2>();
        JokerDescription.instance.SetDescriptionOff();
    }

    public void OnEndDrag(PointerEventData data)
    {
        OnDropEvent?.Invoke();
    }
}
