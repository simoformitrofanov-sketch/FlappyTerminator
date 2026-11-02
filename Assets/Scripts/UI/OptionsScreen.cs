using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _pauseOptionsButton;
    [SerializeField] private Button _gameOverOptionsButton;
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        _pauseOptionsButton.onClick.AddListener(OnOptionsClicked);
        _gameOverOptionsButton.onClick.AddListener(OnOptionsClicked);
        _backButton.onClick.AddListener(OnBackClicked);
        _gameSession.PauseChanged += OnPauseChanged;

        _panel.SetActive(false);
    }

    private void OnDisable()
    {
        _pauseOptionsButton.onClick.RemoveListener(OnOptionsClicked);
        _gameOverOptionsButton.onClick.RemoveListener(OnOptionsClicked);
        _backButton.onClick.RemoveListener(OnBackClicked);
        _gameSession.PauseChanged -= OnPauseChanged;
    }

    private void OnOptionsClicked()
    {
        _pausePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _panel.SetActive(true);
    }

    private void OnBackClicked()
    {
        _panel.SetActive(false);
        _gameOverPanel.SetActive(_gameSession.IsGameOver);
        _pausePanel.SetActive(
            _gameSession.IsPaused && _gameSession.IsGameOver == false);
    }

    private void OnPauseChanged(bool isPaused)
    {
        if (isPaused == false)
            _panel.SetActive(false);
    }
}