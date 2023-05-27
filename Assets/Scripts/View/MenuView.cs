using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MenuView : MonoBehaviour
{
    [SerializeField] private Canvas _root;
    [SerializeField] private MenuAnimation _menuAnimation;
    [SerializeField] private float _menuAnimationTime;
    [SerializeField] private Button _startButton;
    [SerializeField] private List<string> _textFormulas;
    [SerializeField] private TMP_Text[] _textViews;

    private RectTransform _rectTrasform;

    private void OnEnable()
    {
        RectTransform rootTransform = _root.GetComponent<RectTransform>();
        _rectTrasform = GetComponent<RectTransform>();
        _rectTrasform.rect.Set(rootTransform.rect.x, rootTransform.rect.y, rootTransform.rect.width, rootTransform.rect.height);
        transform.localPosition = new Vector3(transform.localPosition.x - _rectTrasform.rect.width, transform.localPosition.y, transform.localPosition.z);
        _menuAnimation.MoveTowards(new Vector2(transform.localPosition.x + _rectTrasform.rect.width, transform.localPosition.y), _menuAnimationTime);

        _startButton.onClick.AddListener(OnStartButtonClick);

        for (int i = 0; i < _textViews.Length; i++)
            _textViews[i].text = _textFormulas[i];
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        _menuAnimation.MoveTowards(new Vector2(transform.localPosition.x - _rectTrasform.rect.width, transform.localPosition.y), _menuAnimationTime, () => gameObject.SetActive(false));
    }
}
