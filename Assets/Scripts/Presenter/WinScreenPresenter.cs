using UnityEngine;
using UnityEngine.Events;

public class WinScreenPresenter : MonoBehaviour
{
    [SerializeField] private WinScreenView _winScreenView;

    public event UnityAction RestartingGame;

    private void OnEnable()
    {
        _winScreenView.RestartingGame += OnRestartingGame;
    }

    private void OnDisable()
    {
        _winScreenView.RestartingGame -= OnRestartingGame;
    }

    public void ShowScreen()
    {
        _winScreenView.gameObject.SetActive(true);
    }

    private void OnRestartingGame()
    {
        RestartingGame?.Invoke();
    }
}
