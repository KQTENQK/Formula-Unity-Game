using System;
using System.Collections;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    private Coroutine _activeCoroutine;
    private Vector2 _normalPosition;

    private void Awake()
    {
        _normalPosition = transform.localPosition;
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        transform.localPosition = _normalPosition;
    }

    public void MoveTowards(Vector2 position, float time, Action whenEnd = null)
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(BeginMovingTowards(position, time, whenEnd));
    }

    private IEnumerator BeginMovingTowards(Vector2 targetPosition, float time, Action whenEnd = null)
    {
        Vector2 startPosition = transform.localPosition;

        for (float elapsed = 0; elapsed < time; elapsed += Time.deltaTime)
        {
            transform.localPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / time);
            yield return null;
        }

        transform.localPosition = targetPosition;
        whenEnd?.Invoke();
    }
}
