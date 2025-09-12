using UnityEngine;

[CreateAssetMenu(fileName = "Destroy Joker", menuName = "Scriptables/Joker/Effect/Handicap/Destroy Joker")]
public class DestroyJokerEffect : JokerEffect
{
    public bool right;
    public bool random;
    public override void ApplyEffect(JokerInstance instance)
    {
        if (right)
        {
            DestroyJokerAtRight(instance);
        }

        if (random)
        {
            DestroyRandomJoker(instance);
        }
    }

    private void DestroyRandomJoker(JokerInstance instance)
    {
        if (JokerManager.instance.currentJokers.Count > 1)
        {
            JokerContainer jokerContainer;

            do
            {
                jokerContainer = JokerManager.instance.currentJokers[Random.Range(0, JokerManager.instance.currentJokers.Count)];
            } while (GameObject.ReferenceEquals(jokerContainer,instance.jokerContainer));

            JokerManager.instance.RemoveJoker(jokerContainer);

        }
    }

    private void DestroyJokerAtRight(JokerInstance instance)
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
