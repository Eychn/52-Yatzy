using System.Collections;
using UnityEngine;

using TMPro;

public class Score : ScaleAnim
{
    [SerializeField] private TMP_Text txt = null;
    [SerializeField] private float colorDuration = 0.3f;
    [SerializeField] private float colorSpeed = 0.3f;
    [SerializeField] private Color color = Color.green;

    private int score = 0;
    private Color origin = Color.white;

    protected override void Awake()
    {
        origin = txt.color;
        base.Awake();
    }

    public void Increase()
    {
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        yield return ScaleCoroutine(Start * Scale);

        txt.text = (++score).ToString();
        StartCoroutine(ColorAnim());

        yield return ScaleCoroutine(Start);
    }

    private IEnumerator ColorAnim()
    {
        yield return Lerp(origin, color);
        yield return Lerp(color, origin);
        txt.color = origin;
    }

    private IEnumerator Lerp(Color color0, Color color2)
    {
        float time = 0;
        while (time < colorDuration / 2f)
        {
            txt.color = Color.Lerp(color0, color2, time * colorSpeed);
            yield return null;
            time += Time.deltaTime;
        }
    }
}
