using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private MenuView _menuView;

    public event UnityAction StartingGame;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        StartingGame?.Invoke();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        _menuView.gameObject.SetActive(true);
    }
}
