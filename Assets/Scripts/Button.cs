using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : ScaleAnim, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private UnityEngine.UI.Button.ButtonClickedEvent onClick = new UnityEngine.UI.Button.ButtonClickedEvent();

    private Coroutine coroutine = null;
    private bool over = false;

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

        coroutine = StartCoroutine(ScaleCoroutine(Start * Scale));
    }

    public void OnPointerUp(PointerEventData _)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(ScaleCoroutine(Start));

        if (over)
            onClick.Invoke();

        over = false;
    }
}
