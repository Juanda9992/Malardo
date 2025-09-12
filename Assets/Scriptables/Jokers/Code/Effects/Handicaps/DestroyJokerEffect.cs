using UnityEngine;

[CreateAssetMenu(fileName = "Destroy Joker", menuName = "Scriptables/Joker/Effect/Handicap/Destroy Joker")]
public class DestroyJokerEffect : JokerEffect
{
    public bool right;
    public override void ApplyEffect(JokerInstance instance)
    {
        int siblingIndex = instance.jokerContainer.transform.GetSiblingIndex();
        if (siblingIndex < JokerManager.instance.currentJokers.Count - 1)
        {
            JokerContainer jokerContainer = JokerManager.instance.currentJokers[siblingIndex + 1];
            JokerManager.instance.RemoveJoker(jokerContainer);

            int multEarned = jokerContainer._jokerInstance.sellValue * 2;
            instance.totalMult += multEarned;
            instance.triggerMessage = "+" + multEarned.ToString();
            instance.jokerContainer.TriggerMessage();
            UpdateDescription(instance);
        }
    }

    public override void UpdateDescription(JokerInstance instance)
    {
        instance.jokerDescription = instance.data.description.Replace("_R_", instance.totalMult.ToString());
    }
}
