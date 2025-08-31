using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FullSlotAnimation : MonoBehaviour
{
    public GameObject consumableSlotAnimation,jokerSlotAnimation;

    public static FullSlotAnimation instance;

    void Awake()
    {
        instance = this;
    }


    public void ShowConsumableAnimation()
    {
        StartCoroutine(ShowFullSlot(consumableSlotAnimation));
    }
    
    public void ShowJokerAnimation()
    {
        StartCoroutine(ShowFullSlot(jokerSlotAnimation));
    }

    public IEnumerator ShowFullSlot(GameObject desiredObject)
    {
        desiredObject.gameObject.SetActive(true);
        desiredObject.transform.DOShakePosition(2, 4);
        yield return new WaitForSeconds(0.8f);

        desiredObject.gameObject.SetActive(false);
    }
}
