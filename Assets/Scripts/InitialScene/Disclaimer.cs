using DG.Tweening;
using TMPro;
using UnityEngine;

public class Disclaimer : MonoBehaviour
{
    [SerializeField, TextArea] private string disclaimerMessage;
    [SerializeField] private TextMeshProUGUI disclaimerText;
    [SerializeField] private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        disclaimerText.text = disclaimerMessage;
        canvasGroup.DOFade(1, 1).SetDelay(2);   
    }

}
