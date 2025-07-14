using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker/Template")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    public string description;
    public string triggerMessage;
    public int overrideEffect = 0;
    public JokerRarity jokerRarity;
    public List<JokerTrigger> triggers;
    public List<JokerEffect> effects;
    [HideInInspector] public Transform jokerTransform;
}
public enum JokerRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[System.Serializable]
public class RequiredHandSizeData
{
    public bool active;
    public int minAmmount;
    public int maxAmmount;
}
