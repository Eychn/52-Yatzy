using System;
using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = 5f;
    [SerializeField] private float rollSpeed = 5f;
    [SerializeField] private float rollDuration = 1.0f;
    [SerializeField] private float startDuration = 0.1f;
    [SerializeField] private float stopDuration = 0.1f;
    [SerializeField] private Vector3[] faces = new Vector3[6];
    [SerializeField] private float shakingSpeed = 1f;
    [SerializeField] private float shakingForce = 1f;
    [SerializeField] private float shakingDuration = 0.3f;

    private float rollTime = 0.0f;
    private Quaternion end = Quaternion.identity;

    private bool isShaking = false;
    private float shakingTime = 0f;
    private float shakingStep = 1f;
    private Vector3 startPos = Vector3.zero;

    private Action endRollCallback = null;

    public void Init(Action endRollCallback)
    {
        this.endRollCallback = endRollCallback;
        startPos = transform.localPosition;
    }

    public void Roll(int value)
    {
        rollTime = 0f;
        end = Quaternion.LookRotation(faces[value % 6]);
        enabled = true;
        StartCoroutine(StartRotation());
    }

    public void Shake()
    {
        isShaking = true;
        shakingTime = 0f;
        shakingStep = shakingDuration / shakingSpeed;
    }

    private IEnumerator StartRotation()
    {
        yield return Lerp(startDuration, transform.rotation * Quaternion.Euler(45f, 45f, 45f));

        Vector3 up = transform.up;

        while (rollTime < rollDuration - stopDuration)
        {
            rollTime += Time.deltaTime;
            transform.Rotate(Vector3.up * Time.deltaTime * rollSpeed, Space.World);
            yield return null;
        }
        
        yield return Lerp(rollDuration, end);

        endRollCallback();
    }

    private IEnumerator Lerp(float timer, Quaternion dest)
    {
        Quaternion prev = transform.rotation;
        float t = 0;
        while (rollTime < timer)
        {
            t += Time.deltaTime;
            rollTime += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(prev, dest, transitionSpeed * t);
            yield return null;
        }

        transform.rotation = dest;
    }

    private void Update()
    {
        if (isShaking)
        {
            shakingTime += Time.deltaTime;
            transform.localPosition = startPos + Vector3.right * Mathf.Sin(shakingTime) * shakingForce;

            if (shakingTime >= shakingStep)
                shakingForce *= -1f;

            if (shakingTime >= shakingDuration)
            {
                isShaking = false;
                transform.localPosition = startPos;
            }
        }
    }
}
