using UnityEngine;
using UnityEngine.UI;

public class ToggleUISprite : MonoBehaviour
{
    [SerializeField] private Sprite[] uiIcons;
    [SerializeField] private Image toggleImage;
    [SerializeField] private Toggle toggle;

    void Awake()
    {
        toggle.onValueChanged.AddListener(SetUpValues);
        toggle.onValueChanged.Invoke(toggle.isOn);
    }

    private void SetUpValues(bool value)
    {
        toggleImage.sprite = value ? uiIcons[0] : uiIcons[1];
    }
}
