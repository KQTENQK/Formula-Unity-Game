using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class EndScreenView : MonoBehaviour
{
    [SerializeField] private Canvas _root;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _textField;

    public event UnityAction RestartingGame;

    private void OnEnable()
    {
        RectTransform rootTransform = _root.GetComponent<RectTransform>();
        GetComponent<RectTransform>().rect.Set(rootTransform.rect.x, rootTransform.rect.y, rootTransform.rect.width, rootTransform.rect.height);

        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    public void SetText(string text)
    {
        _textField.text = text;
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnRestartButtonClick()
    {
        RestartingGame?.Invoke();
    }
}
