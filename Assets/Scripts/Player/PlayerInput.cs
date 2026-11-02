using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _shootKey = KeyCode.F;
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;

    public event Action JumpRequested;
    public event Action ShootRequested;
    public event Action PauseRequested;

    public void Read()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            PauseRequested?.Invoke();
            return;
        }

        if (Time.timeScale == 0f)
            return;

        if (Input.GetKeyDown(_jumpKey))
            JumpRequested?.Invoke();

        if (Input.GetKey(_shootKey))
            ShootRequested?.Invoke();
    }
}