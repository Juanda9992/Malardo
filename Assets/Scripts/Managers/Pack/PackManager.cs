using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject packSection;
    [SerializeField] private JokerListContainer jokerListContainer;
    [SerializeField] private GameObject PackInteractablePrefab;
    [SerializeField] private Transform itemsDisplay;
    void Awake()
    {
        instance = this;
    }

    public void ReceiveCreatePackInstruction(PackType packDesired)
    {
        SetAllUIStatus(false);
        switch (packDesired)
        {
            case PackType.Buffon:
                CreateBuffonPack();
                break;
        }
    }
    private void SetAllUIStatus(bool status)
    {
        foreach (var element in otherUI)
        {
            element.SetActive(status);
        }

        packSection.SetActive(!status);
    }

    private void CreateBuffonPack()
    {
        Debug.Log("Created Buffon Pack");

        for (int i = 0; i < 2; i++)
        {
            GameObject item = Instantiate(PackInteractablePrefab, itemsDisplay);
            item.GetComponent<PackInteractable>().SetJokerInfo(jokerListContainer.GetRandomJoker());

        }
    }
}

public enum PackType
{
    None,
    Buffon,
    Tarot,
    Planet,
    Spectral
}
