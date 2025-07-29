using UnityEngine;

[CreateAssetMenu(fileName = "Log", menuName = "Scriptables/Logs/Log")]
public class LogInfo : ScriptableObject
{
    public string versionName;
    [TextArea(10,30)] public string logcontent;
} 
