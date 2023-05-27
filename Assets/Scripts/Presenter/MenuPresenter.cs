using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuPresenter : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private MenuView _menuView;

    public event UnityAction StartingGame;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        StartingGame?.Invoke();
    }

    public void OpenMenu()
    {
        _menuView.gameObject.SetActive(true);
    }
}
