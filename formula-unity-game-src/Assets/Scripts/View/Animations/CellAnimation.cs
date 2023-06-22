using System;
using System.Collections;
using UnityEngine;

public class CellAnimation : MonoBehaviour
{
    public void StartAnimation(Vector3 startPosition, Vector3 endPosition, float secondsToMoveDown)
    {
        StartCoroutine(MoveDown(startPosition, endPosition, secondsToMoveDown));
    }

    public void DestroyAnimation(float secondsToShrink, Action whenEnd = null)
    {
        if (TryGetComponent<ButtonAnimation>(out ButtonAnimation buttonAnimation))
            buttonAnimation.enabled = false;

        StartCoroutine(Shrink(secondsToShrink, whenEnd));
    }

    private IEnumerator MoveDown(Vector3 startPosition, Vector3 endPosition, float secondsToMoveDown)
    {
        const float tMin = 0;
        const float tMax = 1;

        for (float t = tMin; t <= tMax; t += (tMax / secondsToMoveDown) * Time.deltaTime)
        {
            yield return null;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
        }

        transform.localPosition = endPosition;
    }

    private IEnumerator Shrink(float secondsToShrink, Action whenEnd = null)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        for (float elapsed = 0; elapsed <= secondsToShrink; elapsed += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / secondsToShrink);

            yield return null;
        }

        transform.localScale = endScale;
        whenEnd?.Invoke();
    }
}
