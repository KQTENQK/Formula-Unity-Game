using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Button _button;
    private Coroutine _activeCoroutine;

    public event UnityAction  CellClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCellClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCellClick);
    }

    private void OnCellClick()
    {
        CellClick?.Invoke();
    }

    public void ChangeColor(Color startColor, Color targetColor, float transitionDuration)
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(BeginChangingColor(startColor, targetColor, transitionDuration));
    }

    private IEnumerator BeginChangingColor(Color startColor, Color targetColor, float transitionDuration)
    {
        const float tMin = 0;

        Image image = GetComponent<Image>();

        for (float passedTime = tMin; passedTime < transitionDuration; passedTime += Time.deltaTime)
        {
            image.color = Color.Lerp(startColor, targetColor, passedTime / transitionDuration);

            yield return null;
        }

        image.color = targetColor;
        _activeCoroutine = null;
    }

    public void SetText(string text)
    {
        if (_text is null)
            throw new UnityException("Text container was null.");

        _text.text = text;
    }

    public void SetInteractable(bool state)
    {
        _button.interactable = state;
    }
}
