using System.Collections;
using UnityEngine;

public class ScaleAnim : MonoBehaviour
{
    [SerializeField] private float scale = 0.9f;
    [SerializeField] private float epsilon = 0.001f;
    [SerializeField] private float speed = 0.3f;

    private Vector3 start = Vector2.one;
    public Vector3 Start => start;
    public float Scale => scale;

    protected virtual void Awake()
    {
        start = transform.localScale;
    }

    protected IEnumerator ScaleCoroutine(Vector3 dest)
    {
        Vector3 tmp = transform.localScale;
        float t = 0f;

        while ((dest - transform.localScale).magnitude > epsilon)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(tmp, dest, speed * t);
            yield return null;
        }

        transform.localScale = dest;
    }
}
