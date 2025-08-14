using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FullSlotAnimation : MonoBehaviour
{
    public GameObject consumableSlotAnimation;

    public static FullSlotAnimation instance;

    void Awake()
    {
        instance = this;
    }

    public IEnumerator ShowConsumableAnimation()
    {
        consumableSlotAnimation.gameObject.SetActive(true);
        consumableSlotAnimation.transform.DOShakePosition(2,4);
        yield return new WaitForSeconds(0.8f);

        consumableSlotAnimation.gameObject.SetActive(false);
    }
}
