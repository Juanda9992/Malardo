using DG.Tweening;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;
    [SerializeField] private float fadeTime;
    private Camera _camera;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _camera = Camera.main;
    }

    public void SetBgColor(Color color)
    {
        _camera.DOColor(color, fadeTime);
    }
}
