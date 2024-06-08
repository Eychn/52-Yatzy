using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private UnityEngine.UI.Button.ButtonClickedEvent onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
    [SerializeField] private float scale = 0.9f;
    [SerializeField] private float epsilon = 0.001f;
    [SerializeField] private float speed = 0.3f;

    private Vector3 start = Vector2.one;
    private Coroutine coroutine = null;
    private bool over = false;

    private void Awake()
    {
        start = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData _)
    {
        over = true;
    }

    public void OnPointerExit(PointerEventData _)
    {
        over = false;
    }

    public void OnPointerDown(PointerEventData _)
    {
        over = true;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(Scale(start * scale));
    }

    public void OnPointerUp(PointerEventData _)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(Scale(start));

        if (over)
            onClick.Invoke();

        over = false;
    }

    private IEnumerator Scale(Vector3 dest)
    {
        Vector3 tmp = transform.localScale;
        float t = 0f;

        while ((dest - transform.localScale).magnitude > epsilon)
        {
            t += Time.deltaTime;
            transform.localScale = Vector2.Lerp(tmp, dest, speed * t);
            yield return null;
        }

        transform.localScale = dest;
    }
}
