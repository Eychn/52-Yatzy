using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static System.Random random = null;

    [SerializeField] private Dice dice = null;
    [SerializeField] private TMP_Text scoreTxt = null;
    private int score = 0;
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
            scoreTxt.text = (++score).ToString();
    }
}
