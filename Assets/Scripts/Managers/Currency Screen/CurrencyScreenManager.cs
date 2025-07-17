using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

public class CurrencyScreenManager : MonoBehaviour
{
    [SerializeField] private Button nextRoundButton;
    [SerializeField] private TextMeshProUGUI nextRoundButtonText;
    [SerializeField] private Transform currencyParent;
    [SerializeField] private GameObject currencyRowPrefab;
    [SerializeField] private UnityEvent OnCashOut;
    private int roundScore;
    void Awake()
    {
        nextRoundButton.onClick.AddListener(CashMoney);
    }
    void OnEnable()
    {
        StartCoroutine(nameof(SetUpCurrencyScreen));
    }
    private IEnumerator SetUpCurrencyScreen()
    {
        yield return new WaitForEndOfFrame();
        CalculateScore();
        CalculateInvest();

        UpdateScoreButtonText();
    }
    private void CalculateScore()
    {
        int handsRemaining = HandManager.instance.hands;
        roundScore = 0;
        roundScore += handsRemaining;

        GameObject row = Instantiate(currencyRowPrefab, currencyParent);
        row.GetComponent<CurrencyIndicator>().SetIndicator(handsRemaining, "Hands Remaining ($1 per remaining)", handsRemaining, Color.blue);
    }

    private void CalculateInvest()
    {
        int invest = Mathf.FloorToInt(CurrencyManager.instance.currentCurrency / 5);
        roundScore += invest;
        if (invest > 0)
        {
            GameObject row = Instantiate(currencyRowPrefab, currencyParent);
            row.GetComponent<CurrencyIndicator>().SetIndicator(invest, "Invest ($1 per $5)", invest, Color.yellow);
        }
    }
    private void UpdateScoreButtonText()
    {
        nextRoundButtonText.text = "Cash Out: $" + roundScore;
    }
    private void CashMoney()
    {
        CurrencyManager.instance.AddCurrency(roundScore);
        OnCashOut?.Invoke();
    }
}
