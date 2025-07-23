using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public GameObject interactButton;

    private bool selected;

    public void SwitchSelection()
    {
        selected = !selected;

        interactButton.SetActive(selected);
    }
}
