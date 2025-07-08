using System;
using TMPro;
using UnityEngine;

public class JokerContainer : MonoBehaviour
{
    public JokerData _joker;

    public Action JokerAction;

    [SerializeField] private TextMeshProUGUI jokerText; 

    void Start()
    {
        Debug.Log(_joker.jokerName);
        SetUpJoker();
    }

    public void SetUpJoker()
    {
        jokerText.text = _joker.jokerName;

        JokerAction += () => ScoreSign.instance.SetJokerSign(_joker.triggerMessage, transform.position);
        JokerParser.instance.ParseJoker(this);
    }

    [ContextMenu("Test Joker")]
    private void TestJoker()
    {
        JokerAction?.Invoke();
    }
}
