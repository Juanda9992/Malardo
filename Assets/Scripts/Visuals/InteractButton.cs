using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject sellButton;

    private bool selected;

    public void SwitchSelection()
    {
        selected = !selected;

        interactButton.SetActive(selected);

        if (sellButton == null) return;
        sellButton.SetActive(selected);
    }

    public void SetSelection(bool status)
    {
        selected = status;
        interactButton.SetActive(status);

        if (sellButton == null) return;
        sellButton.SetActive(selected);
    }
}
