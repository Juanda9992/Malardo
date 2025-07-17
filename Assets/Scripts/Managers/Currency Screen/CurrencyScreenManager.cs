using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        roundScore = 0;
        roundScore += HandManager.instance.hands;
    }

    private void CalculateInvest()
    {
        int invest = Mathf.FloorToInt(CurrencyManager.instance.currentCurrency / 5);
        roundScore += invest;
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
