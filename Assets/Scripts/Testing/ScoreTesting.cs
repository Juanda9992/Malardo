using UnityEngine;

public class ScoreTesting : MonoBehaviour
{
    [SerializeField] private int mult;
    [SerializeField] private int chips;

    [ContextMenu("Add Mult")]
    private void AddMult()
    {
        ScoreManager.instance.AddMult(mult);
    }

    [ContextMenu("Add Chips")]
    private void AddChips()
    {
        ScoreManager.instance.AddChips(chips);
    }
}
