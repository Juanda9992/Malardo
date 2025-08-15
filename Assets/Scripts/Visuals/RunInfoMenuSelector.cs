
using UnityEngine;

public class RunInfoMenuSelector : MonoBehaviour
{
    [SerializeField] private RunMenu[] menus;

    public void SelectMenu(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].buttonIndicator.SetActive(false);
            menus[i].menuToOpen.SetActive(false);
        }

        menus[index].SelectMenu();
    }
}

[System.Serializable]
public class RunMenu
{
    public GameObject menuToOpen;
    public GameObject buttonIndicator;

    public void SelectMenu()
    {
        menuToOpen.SetActive(true);
        buttonIndicator.SetActive(true);
    }
}
