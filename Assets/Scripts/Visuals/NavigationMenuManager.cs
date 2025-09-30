using UnityEngine;

public class NavigationMenuManager : MonoBehaviour
{
    public static NavigationMenuManager instance;
    public GameObject lastMenu;

    void Awake()
    {
        instance = this;
    }
    public void OpenLastMenu()
    {
        lastMenu.SetActive(true);
    }
}
