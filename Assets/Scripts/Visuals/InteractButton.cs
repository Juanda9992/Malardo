using UnityEngine;
using UnityEngine.Events;

public class InteractButton : MonoBehaviour
{
    public UnityEvent OnSelected;
    public GameObject interactButton;
    public GameObject sellButton;

    private bool selected;

    public void SwitchSelection()
    {
        selected = !selected;

        interactButton.SetActive(selected);

        if (selected)
        {
            OnSelected?.Invoke();
        }

        if (sellButton == null) return;
        sellButton.SetActive(selected);
    }

    public void SetSelection(bool status)
    {
        selected = status;
        interactButton.SetActive(status);

        if (status)
        {
            OnSelected?.Invoke();
        }

        if (sellButton == null) return;
        sellButton.SetActive(selected);
    }

    public void SetInteractButton(GameObject button)
    {
        interactButton = button;
    }

    public void SetSecondaryButton(GameObject button)
    {
        sellButton = button;
    }
}
