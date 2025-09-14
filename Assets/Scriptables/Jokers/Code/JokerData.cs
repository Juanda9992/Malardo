using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Joker", menuName = "Scriptables/Joker/Template")]
public class JokerData : ScriptableObject
{
    public string jokerName;
    [TextArea] public string description;
    public string triggerMessage;
    public float overrideEffect = 0;
    public int shopValue;
    public Sprite JokerImage;
    public JokerRarity jokerRarity;
    public JokerLogic[] jokerLogics;
    public List<JokerEffect> OnSetUpJoker;
    public List<JokerEffect> OnSellEffect;
    public JokerEffect reactivationJoker;
}

[System.Serializable]
public class JokerInstance
{
    public JokerContainer jokerContainer;
    public string jokerDescription;
    public string triggerMessage;
    public JokerData data;

    public int totalChips;
    public float totalMult;
    public float totalMultiplier;
    public int dynamicVariable;
    public CardEdition jokerEdition = CardEdition.Base;
    public JokerInstance(JokerData jokerData)
    {
        data = jokerData;
        jokerDescription = data.description;
        triggerMessage = data.triggerMessage;
        jokerLogics = jokerData.jokerLogics;
        SetEdition();
    }

    public void SetInstanceData(JokerInstance jokerInstance)
    {
        totalChips = jokerInstance.totalChips;
        totalMult = jokerInstance.totalMult;
        totalMultiplier = jokerInstance.totalMultiplier;
        dynamicVariable = jokerInstance.dynamicVariable;
        jokerEdition = jokerInstance.jokerEdition;
    }

    public void SetJokerContainer(JokerContainer container)
    {
        jokerContainer = container;
    }

    private void SetEdition()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random == 0)
        {
            this.jokerEdition = CardEdition.Polychrome;
        }
        else if (random < 2)
        {
            this.jokerEdition = CardEdition.Holographic;
        }
        else if (random < 5)
        {
            this.jokerEdition = CardEdition.Foil;
        }
        else if (random < 6)
        {
            this.jokerEdition = CardEdition.Negative;
        }
    }

    public JokerLogic[] jokerLogics;
    public bool destroyJoker;
    public int extraSellValue = 0;
    public int sellValue { get { return Mathf.FloorToInt((float)data.shopValue / 2) + extraSellValue; } }
}

[System.Serializable]
public class JokerLogic
{
    public TriggerEvent triggerEvent;
    public bool CanBetriggered()
    {
        for (int i = 0; i < jokerTrigger.Length; i++)
        {
            if (!jokerTrigger[i].MeetCondition(GameStatusManager._Status))
            {
                return false;
            }
        }
        return true;
    }
    public JokerTrigger[] jokerTrigger;
    public JokerEffect[] jokerEffect;
}

public enum TriggerEvent
{
    BlindSelected = 0,
    RoundBegin = 1,
    OnHandPlay = 2,
    OnHandEnd = 3,
    OnCardPlay = 4,
    OnBlindDefeated = 5,
    OnHandDiscard = 6,
    BeforeHandPlay = 7,
    OnPlanetCardUse = 8,
    OnPackSkipped = 9,
    OnCardAdded = 10,
    OnLuckyCardPlay = 11,
    OnShopReroll = 12,
    OnCardDiscard = 13,
    OnGlassCardDestroyed = 14,
    OnTarotCardUsed = 15,
    OnBoosterPackOpened = 16,
    OnCardDestroyed = 17,
    OnCardSold = 18
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

