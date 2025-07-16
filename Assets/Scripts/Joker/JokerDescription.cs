using TMPro;
using UnityEngine;

public class JokerDescription : MonoBehaviour
{
    public static JokerDescription instance;

    void Awake()
    {
        instance = this;
        SetDescriptionOff();
    }
    [SerializeField] private TextMeshProUGUI descriptionText;
    public void SetDescriptionOn(JokerData jokerData, Vector2 jokerPos)
    {
        transform.localScale = Vector2.one;
        descriptionText.text = jokerData.description;
        transform.position = jokerPos;
    }

    public void SetDescriptionOff()
    {
        transform.localScale = Vector2.zero;
    }
}
