using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    void Awake()
    {
        inputActions.Enable();
    }
}
