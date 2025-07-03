using System;
using UnityEngine;

public class JokerContainer : MonoBehaviour
{
    public JokerData _joker;

    public Action JokerAction;

    void Start()
    {
        Debug.Log(_joker.jokerName);
        SetUpJoker();
    }

    public void SetUpJoker()
    {
        JokerParser.instance.ParseJoker(this);
    }

    [ContextMenu("Test Joker")]
    private void TestJoker()
    {
        JokerAction?.Invoke();
    }
}
