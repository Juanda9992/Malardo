using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JokerButtonActions : MonoBehaviour
{
    [SerializeField] private JokerContainer jokerContainer;
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText;
    private bool selected = false;

    public void SwitchSelectedLogic()
    {
        selected = !selected;
        if (selected)
        {
            SetButtonState();
        }
        else
        {
            actionButton.gameObject.SetActive(false);
        }
    }
    private void SetButtonState()
    {
        actionButton.gameObject.SetActive(true);
        if (jokerContainer.isOnShop)
        {
            actionButtonText.text = "Buy";
        }
        else
        {
            actionButtonText.text = "Sell";
        }
    }
}
