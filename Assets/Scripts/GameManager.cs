using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static System.Random random = null;

    [SerializeField] private Dice dice = null;
    [SerializeField] private Score score = null;
    private bool win = false;
    private bool canRoll = true;

    private void Awake()
    {
        dice.Init(OnEndRoll);
        random = new System.Random();
    }

    public void Roll()
    {
        if (canRoll)
        {
            canRoll = false;
            int res = random.Next(0, 6);
            win = res == 5;
            dice.Roll(res);
        }
    }

    private void OnEndRoll()
    {
        canRoll = true;

        if (win)
        {
            score.Increase();
            dice.Shake();
        }
    }
}
