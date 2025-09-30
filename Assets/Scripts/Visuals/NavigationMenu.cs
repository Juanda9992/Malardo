using UnityEngine;

public class NavigationMenu : MonoBehaviour
{
    void OnEnable()
    {
        NavigationMenuManager.instance.lastMenu = this.gameObject;
    }
}
