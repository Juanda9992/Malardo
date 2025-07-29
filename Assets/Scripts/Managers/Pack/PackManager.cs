using TMPro;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject packSection;
    [SerializeField] private JokerListContainer jokerListContainer;
    [SerializeField] private GameObject PackInteractablePrefab;
    [SerializeField] private GameObject cardPackInteractablePrefab;
    [SerializeField] private Transform itemsDisplay;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI packNameLabel;
    [SerializeField] private TextMeshProUGUI selectAmmountLabel;
    private int maxSelections = 1;
    [SerializeField] private PackData testPack;
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

        CreatePack(packDesired.numberOfCards, packDesired.packType);
    }
    private void SetAllUIStatus(bool status)
    {
        foreach (var element in otherUI)
        {
            element.SetActive(status);
        }

        packSection.SetActive(!status);
    }

    private void CreatePack(int cardsToCreate, PackType packType)
    {
        for (int i = 0; i < cardsToCreate; i++)
        {

            if (packType == PackType.Buffon)
            {
                BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.buffonPackBgColor);
                GameObject item = Instantiate(PackInteractablePrefab, itemsDisplay);
                item.GetComponent<PackInteractable>().SetJokerInfo(jokerListContainer.GetRandomJoker());
            }
            else if (packType == PackType.Card)
            {
                BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.cardPackBgColor);
                GameObject item = Instantiate(cardPackInteractablePrefab, itemsDisplay);
                item.GetComponent<PackInteractable>().SetPackCard();
            }

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
        BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.defaultBgColor);
    }
    [ContextMenu("Create pack")]
    private void TestPack()
    {
        ReceiveCreatePackInstruction(testPack);
    }
}

public enum PackType
{
    None,
    Buffon,
    Tarot,
    Planet,
    Spectral,
    Card
}
