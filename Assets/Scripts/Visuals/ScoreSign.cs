using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;
public class ScoreSign : MonoBehaviour
{
    [SerializeField] private float increaseTime;
    [SerializeField] private float timetoHide;
    [SerializeField] private TextMeshProUGUI scoreSignText;

    [SerializeField] private Color chipColor, multColor;
    [SerializeField] private Image bgImage;
    public static ScoreSign instance;

    void Awake()
    {
        instance = this;
        transform.DOScale(0, 0f);
    }
    public void SetScoreSign(Card card)
    {
        StopCoroutine(nameof(AutoHide));

        bgImage.color = chipColor;
        HandleHideAnim();
        transform.position = card.linkedCard.transform.position;
        scoreSignText.text = card.number.ToString();
    }
    public void SetJokerSign(string message, Vector2 jokerPos)
    {
        StopCoroutine(nameof(AutoHide));

        HandleHideAnim();
        scoreSignText.text = message;
        bgImage.color = multColor;
        transform.position = jokerPos;
    }
    private void HandleHideAnim()
    {
        transform.DOScale(0, 0);
        transform.DOScale(1, increaseTime).OnComplete(()=>StartCoroutine(nameof(AutoHide)));
    }

    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(timetoHide);
        transform.DOScale(0, 0.3f);
    }
}
