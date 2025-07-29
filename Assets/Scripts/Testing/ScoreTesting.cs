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
    [ContextMenu("Generate Cards")]
    private void TestCardTypes()
    {
        Card card = new Card();
        for (int i = 0; i < 1000; i++)
        {
            card = card.GenerateRandomCard();
            Debug.Log(card.cardEdition);
            Debug.Log(card.cardSeal);
            Debug.Log(card.cardType);
        }
    }
}
