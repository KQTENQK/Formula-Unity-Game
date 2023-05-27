using System.Collections;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Vector2 _targetScale;
    [SerializeField] private float _halfWayChangingTime;

    private Coroutine _activeCoroutine;
    private Vector2 _normalScale;

    private void Awake()
    {
        _normalScale = transform.localScale;
    }

    private void OnEnable()
    {
        _activeCoroutine = StartCoroutine(BeginAnimation(_targetScale, _halfWayChangingTime));
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        transform.localScale = _normalScale;
    }

    private IEnumerator BeginAnimation(Vector2 targetScale, float halfWayChangingTime)
    {
        Vector2 startScale = _normalScale;
        Vector2 endScale = targetScale;

        while (true)
        {
            for (float passedTime = 0; passedTime < halfWayChangingTime; passedTime += Time.deltaTime)
            {
                transform.localScale = Vector2.Lerp(startScale, endScale, passedTime / halfWayChangingTime);
                yield return null;
            }

            transform.localScale = endScale;

            Vector2 temp = endScale;
            endScale = startScale;
            startScale = temp;

            yield return null;
        }
    }
}
