using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _resumeButton;

    private void OnEnable()
    {
        _gameSession.PauseChanged += OnPauseChanged;
        _resumeButton.onClick.AddListener(OnResumeClicked);

        _panel.SetActive(_gameSession.IsPaused);
    }

    private void OnDisable()
    {
        _gameSession.PauseChanged -= OnPauseChanged;
        _resumeButton.onClick.RemoveListener(OnResumeClicked);
    }

    private void OnPauseChanged(bool isPaused)
    {
        _panel.SetActive(isPaused);
    }

    private void OnResumeClicked()
    {
        _gameSession.SetPaused(false);
    }
}