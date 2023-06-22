using UnityEngine;
using UnityEngine.Events;

public class EndScreenPresenter : MonoBehaviour
{
    [SerializeField] private EndScreenView _winScreenView;

    public event UnityAction RestartingGame;

    private void OnEnable()
    {
        _winScreenView.RestartingGame += OnRestartingGame;
    }

    private void OnDisable()
    {
        _winScreenView.RestartingGame -= OnRestartingGame;
    }

    public void ShowScreen(string text)
    {
        _winScreenView.gameObject.SetActive(true);
        _winScreenView.SetText(text);
    }

    private void OnRestartingGame()
    {
        RestartingGame?.Invoke();
    }
}
