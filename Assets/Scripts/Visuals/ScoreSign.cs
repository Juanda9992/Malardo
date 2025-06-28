using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
public class ScoreSign : MonoBehaviour
{
    [SerializeField] private float increaseTime;
    [SerializeField] private float timetoHide;
    [SerializeField] private TextMeshProUGUI scoreSignText;

    public static ScoreSign instance;

    void Awake()
    {
        instance = this;
    }
    public void SetScoreSign(Card card)
    {
        StopCoroutine(nameof(AutoHide));

        transform.position = card.linkedCard.transform.position;
        transform.DOScale(0, 0);
        transform.DOScale(1, increaseTime).OnComplete(()=>StartCoroutine(nameof(AutoHide)));

        scoreSignText.text = card.number.ToString();
    }

    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(timetoHide);
        transform.DOScale(0, 0.3f);
    }
}
