using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public bool useAltColors = false;

    public static Action<bool> OnHighContrastChanged;
    void Awake()
    {
        instance = this;
    }

    public void SetHighContrastColors(bool status)
    {
        useAltColors = status;
        OnHighContrastChanged?.Invoke(useAltColors);
    }
}
