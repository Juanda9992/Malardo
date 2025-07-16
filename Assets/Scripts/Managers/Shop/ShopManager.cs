using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject jokerGenerator;

    [ContextMenu("Generate Joker")]
    public void SetGenerateJokersAction()
    {
        jokerGenerator.SetActive(true);
    }
}
