using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenAnimation : MonoBehaviour
{
    [SerializeField] private Canvas _root;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _restartButton;
    [SerializeField] private float _animationTime;
    [SerializeField] private Image _panelImage;
    [SerializeField] private float _changingAlphaDuration;

    private float _startAlpha;
    private float _endAlpha;
    private float _yTextPivot;
    private float _yRestartButtonPivot;
    private float _yParentPivot;
    private Vector2 _textInSightPosition;
    private Vector2 _restartButtonInSightPosition;
    private Vector2 _textOutSightPosition;
    private Vector2 _restartButtonOutSightPosition;
    private System.Collections.Generic.List<Coroutine> _activeCoroutines;
    private RectTransform _restartButtonRectTransform;

    private void Awake()
    {
        _activeCoroutines = new System.Collections.Generic.List<Coroutine>();
        _startAlpha = 0;
        _endAlpha = _panelImage.color.a;
        _yParentPivot = GetComponentInParent<RectTransform>().pivot.y;
        _yTextPivot = _text.rectTransform.pivot.y;
        _restartButtonRectTransform = _restartButton.GetComponent<RectTransform>();
        _textInSightPosition = _text.rectTransform.localPosition;
        _restartButtonInSightPosition = _restartButtonRectTransform.localPosition;
        RectTransform rootTransform = _root.GetComponent<RectTransform>();
        _yRestartButtonPivot = _restartButtonRectTransform.pivot.y;
        _textOutSightPosition = new Vector2(_text.rectTransform.localPosition.x,
            rootTransform.rect.height * _yParentPivot + _text.rectTransform.rect.height * _yTextPivot);
        _restartButtonOutSightPosition = new Vector2(_restartButtonRectTransform.localPosition.x,
            -(rootTransform.rect.height) * _yParentPivot - _restartButtonRectTransform.rect.height * _yRestartButtonPivot);
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);

        _text.rectTransform.localPosition = _textOutSightPosition;
        _restartButtonRectTransform.localPosition = _restartButtonOutSightPosition;

        MoveInSight();
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);

        for (int i = 0; i < _activeCoroutines.Count; i++)
            StopCoroutine(_activeCoroutines[i]);

        _activeCoroutines.Clear();
    }

    public void MoveInSight(Action whenEnd = null)
    {
        for (int i = 0; i < _activeCoroutines.Count; i++)
            StopCoroutine(_activeCoroutines[i]);

        _activeCoroutines.Clear();

        _activeCoroutines.Add(StartCoroutine(MoveTowards(_text.rectTransform, _textOutSightPosition, _textInSightPosition, _animationTime, whenEnd)));
        _activeCoroutines.Add(StartCoroutine(MoveTowards(_restartButtonRectTransform, _restartButtonOutSightPosition, _restartButtonInSightPosition, _animationTime, whenEnd)));
        _activeCoroutines.Add(StartCoroutine(LerpAlpha(_startAlpha, _endAlpha, _changingAlphaDuration)));
    }

    public void MoveOutSight(Action whenEnd = null)
    {
        for (int i = 0; i < _activeCoroutines.Count; i++)
            StopCoroutine(_activeCoroutines[i]);

        _activeCoroutines.Clear();

        _activeCoroutines.Add(StartCoroutine(MoveTowards(_text.rectTransform, _textInSightPosition, _textOutSightPosition, _animationTime, whenEnd)));
        _activeCoroutines.Add(StartCoroutine(MoveTowards(_restartButtonRectTransform, _restartButtonInSightPosition, _restartButtonOutSightPosition, _animationTime, whenEnd)));
        _activeCoroutines.Add(StartCoroutine(LerpAlpha(_endAlpha, _startAlpha, _changingAlphaDuration)));
    }

    private IEnumerator MoveTowards(RectTransform transform, Vector2 startPosition, Vector2 targetPosition, float time, Action whenEnd = null)
    {
        for (float elapsed = 0; elapsed < time; elapsed += Time.deltaTime)
        {
            transform.localPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / time);

            yield return null;
        }

        transform.localPosition = targetPosition;
        whenEnd?.Invoke();
    }

    private IEnumerator LerpAlpha(float startAlpha, float endAlpha, float time)
    {
        Color startColor = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, startAlpha);
        Color endColor = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, endAlpha);

        for (float elapsed = 0; elapsed < time; elapsed += Time.deltaTime)
        {
            _panelImage.color = Color.Lerp(startColor, endColor, elapsed / time);

            yield return null;
        }

        _panelImage.color = endColor;
    }

    private void OnRestartButtonClick()
    {
        MoveOutSight(() => gameObject.SetActive(false));
    }
}
