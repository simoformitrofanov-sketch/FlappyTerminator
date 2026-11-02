using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private Player _player;

    public event Action GameOver;
    public event Action<bool> PauseChanged;

    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _player.PauseRequested += OnPauseRequested;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _player.PauseRequested -= OnPauseRequested;
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPaused(bool isPaused)
    {
        if (IsGameOver || IsPaused == isPaused)
            return;

        IsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        PauseChanged?.Invoke(IsPaused);
    }

    private void OnPlayerDied()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        Time.timeScale = 0f;

        GameOver?.Invoke();
    }

    private void OnPauseRequested()
    {
        SetPaused(IsPaused == false);
    }
}