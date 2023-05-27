using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class WinScreenView : MonoBehaviour
{
    [SerializeField] private Canvas _root;
    [SerializeField] private Button _restartButton;

    public event UnityAction RestartingGame;

    private void OnEnable()
    {
        RectTransform rootTransform = _root.GetComponent<RectTransform>();
        GetComponent<RectTransform>().rect.Set(rootTransform.rect.x, rootTransform.rect.y, rootTransform.rect.width, rootTransform.rect.height);

        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        RestartingGame?.Invoke();
    }
}
