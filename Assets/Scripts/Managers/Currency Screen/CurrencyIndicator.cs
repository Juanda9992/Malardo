using TMPro;
using UnityEngine;

public class CurrencyIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indicatorText;
    [SerializeField] private TextMeshProUGUI indicatorDescription;
    [SerializeField] private int moneyChars;

    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private Transform moneyParent;

    public void SetIndicator(int indicatorValue, string description, int moneyChars, Color indicatorColor)
    {
        indicatorText.color = indicatorColor;

        indicatorText.text = indicatorValue.ToString();
        indicatorDescription.text = description;

        for (int i = 0; i < moneyChars; i++)
        {
            Instantiate(moneyPrefab, moneyParent);
        }
    }
}
