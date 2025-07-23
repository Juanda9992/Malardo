using UnityEngine;

public class PackInteractable : MonoBehaviour
{
    public PackType itemType;
    public JokerData jokerData;

    public void SetJokerInfo(JokerData createdJoker)
    {
        jokerData = createdJoker;
        GetComponent<DescriptionContainer>().SetNameAndDescription(jokerData.jokerName, jokerData.description);
    }

    public void SelectItem()
    {
        switch (itemType)
        {
            case PackType.Buffon:
                JokerManager.instance.AddJoker(jokerData);
                break;
        }
    }
}
