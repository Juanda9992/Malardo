using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Log Story",menuName = "Scriptables/Logs/Log Story")]
public class LogContainer : ScriptableObject
{
    public List<LogInfo> logStory;
}
