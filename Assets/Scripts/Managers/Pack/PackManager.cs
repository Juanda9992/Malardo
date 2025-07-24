using TMPro;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject packSection;
    [SerializeField] private JokerListContainer jokerListContainer;
    [SerializeField] private GameObject PackInteractablePrefab;
    [SerializeField] private Transform itemsDisplay;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI packNameLabel;
    [SerializeField] private TextMeshProUGUI selectAmmountLabel;
    private int maxSelections = 1;
    void Awake()
    {
        instance = this;
    }

    public void ReceiveCreatePackInstruction(PackData packDesired)
    {
        maxSelections = packDesired.selectCards;
        SetAllUIStatus(false);

        packNameLabel.text = packDesired.packName;
        selectAmmountLabel.text = "Choose " + maxSelections;

        switch (packDesired.packType)
        {
            case PackType.Buffon:
                CreateBuffonPack(packDesired.numberOfCards);
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

    private void CreateBuffonPack(int cardsToCreate)
    {
        for (int i = 0; i < cardsToCreate; i++)
        {
            GameObject item = Instantiate(PackInteractablePrefab, itemsDisplay);
            item.GetComponent<PackInteractable>().SetJokerInfo(jokerListContainer.GetRandomJoker());

        }
    }

    public void SelectItem()
    {
        maxSelections--;

        if (maxSelections == 0)
        {
            SkipPackage();
        }
        selectAmmountLabel.text = "Choose " + maxSelections;
    }

    //CALLED BY UI BUTTON
    public void SkipPackage()
    {
        SetAllUIStatus(true);
        CardManager.DestroyChildsInParent(itemsDisplay);
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
