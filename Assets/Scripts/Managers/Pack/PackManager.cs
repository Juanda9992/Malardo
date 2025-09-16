using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PackManager : MonoBehaviour
{
    public bool isOnPackMenu;
    public static PackManager instance;
    [SerializeField] private GameObject[] otherUI;
    [SerializeField] private GameObject packSection;
    [SerializeField] private JokerListContainer jokerListContainer;
    [SerializeField] private GameObject PackInteractablePrefab;
    [SerializeField] private GameObject cardPackInteractablePrefab;
    public CardManipulationManager cardManipulationManager;
    [SerializeField] private Transform itemsDisplay;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI packNameLabel;
    [SerializeField] private TextMeshProUGUI selectAmmountLabel;
    [SerializeField] private GameObject cardManipulationContainer;
    [SerializeField] private GameObject defaultContainer;
    public int maxSelections = 1;
    [SerializeField] private PackData testPack;
    void Awake()
    {
        instance = this;
    }

    public void ReceiveCreatePackInstruction(PackData packDesired)
    {
        maxSelections = packDesired.selectCards;

        SetAllUIStatus(false);
        if (packDesired.packType == PackType.Tarot || packDesired.packType == PackType.Spectral)
        {
            cardManipulationContainer.SetActive(true);
            cardManipulationManager.SetCardLogic(packDesired.numberOfCards,packDesired.packType);
        }
        else
        {
            defaultContainer.SetActive(true);
            CreatePack(packDesired.numberOfCards, packDesired.packType);
        }
        packNameLabel.text = packDesired.packName;
        selectAmmountLabel.text = "Choose " + maxSelections;
        isOnPackMenu = true;

        StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnBoosterPackOpened));
    }
    private void SetAllUIStatus(bool status)
    {
        defaultContainer.SetActive(false);
        cardManipulationContainer.SetActive(false);
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
                GameObject item = Instantiate(PackInteractablePrefab, itemsDisplay);
                BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.buffonPackBgColor);
                item.GetComponent<PackInteractable>().SetJokerInfo(jokerListContainer.GetRandomJoker());
            }
            else if (packType == PackType.Card)
            {
                GameObject item = Instantiate(cardPackInteractablePrefab, itemsDisplay);
                BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.cardPackBgColor);
                item.GetComponent<PackInteractable>().SetPackCard();
            }
            else if (packType == PackType.Planet)
            {
                GameObject item = Instantiate(PackInteractablePrefab, itemsDisplay);
                BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.planetPackBgColor);
                item.GetComponent<PackInteractable>().SetPlanetCard(DatabaseManager.instance.planetCardsDatabase.GetRandomPlanetCard());
            }
        }
    }

    public void SelectItem()
    {
        StartCoroutine("SelectItemWithDelay");
    }
    private IEnumerator SelectItemWithDelay()
    {
        maxSelections--;
        selectAmmountLabel.text = "Choose " + maxSelections;
        JokerDescription.instance.SetDescriptionOff();
        yield return new WaitForSeconds(1.5f);
        if (maxSelections == 0)
        {
            CommonOperations.DestroyChildsInParent(cardManipulationManager.cardEffectParent);
            SkipPackage(false);
            JokerDescription.instance.SetDescriptionOff();
        }

    }

    //CALLED BY UI BUTTON
    public void SkipPackage(bool effect = true)
    {
        if (effect)
        {
            StartCoroutine(JokerManager.instance.PlayJokersAtTime(TriggerEvent.OnPackSkipped));
        }
        SetAllUIStatus(true);
        CommonOperations.DestroyChildsInParent(itemsDisplay);
        CommonOperations.DestroyChildsInParent(cardManipulationManager.layoutGroup.transform);
        CardManager.instance.cardsOnScreen.Clear();
        HandManager.instance.handCards.Clear();
        BackgroundManager.instance.SetBgColor(DatabaseManager.instance.cardColorDatabase.defaultBgColor);
        isOnPackMenu = false;
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
