using UnityEngine;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    public int maxItemsOnShop = 2;
    public static ShopManager instance;
    [SerializeField] private GameObject jokerGenerator;

    [SerializeField] private UnityEvent showShopEvent;
    void Awake()
    {
        instance = this;
    }

    [ContextMenu("Generate Joker")]
    public void SetGenerateJokersAction()
    {
        jokerGenerator.SetActive(true);
    }

    public void IncreaseShopSlots()
    {
        maxItemsOnShop++;
        SetGenerateJokersAction();
    }

    [ContextMenu("Show Shop")]
    public void ShowShop()
    {
        showShopEvent?.Invoke();
        jokerGenerator.SetActive(true);
    }
}
