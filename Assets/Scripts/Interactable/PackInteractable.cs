using UnityEngine;
using UnityEngine.UI;

public class PackInteractable : MonoBehaviour
{
    public PackType itemType;
    public JokerData jokerData;
    public PackData packData;

    [SerializeField] private Button actionButton;

    public void SetJokerInfo(JokerData createdJoker)
    {
        jokerData = createdJoker;
        itemType = PackType.Buffon;
        GetComponent<DescriptionContainer>().SetNameAndDescription(jokerData.jokerName, jokerData.description);
        ListenForAvaliability();
    }

    public void ListenForAvaliability()
    {
        if (itemType == PackType.Buffon)
        {
            actionButton.interactable = JokerManager.instance.CanAddJoker();
        }
    }
    public void SelectItem()
    {
        switch (itemType)
        {
            case PackType.Buffon:
                JokerManager.instance.AddJoker(jokerData);
                break;
        }

        PackManager.instance.SelectItem();
    }
}
