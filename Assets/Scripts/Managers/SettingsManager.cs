using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;


    public bool useAltColors = false;
    void Awake()
    {
        instance = this;
    }
}
