using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _gameSession.GameOver += OnGameOver;
        _restartButton.onClick.AddListener(OnRestartClicked);

        _panel.SetActive(_gameSession.IsGameOver);
    }

    private void OnDisable()
    {
        _gameSession.GameOver -= OnGameOver;
        _restartButton.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnGameOver()
    {
        _panel.SetActive(true);
    }

    private void OnRestartClicked()
    {
        _gameSession.Restart();
    }
}