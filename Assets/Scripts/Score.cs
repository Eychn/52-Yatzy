using System.Collections;
using UnityEngine;

using TMPro;

public class Score : ScaleAnim
{
    [SerializeField] private TMP_Text txt = null;
    private int score = 0;

    public void Increase()
    {
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        yield return ScaleCoroutine(Start * Scale);

        txt.text = (++score).ToString();

        yield return ScaleCoroutine(Start);
    }
}
