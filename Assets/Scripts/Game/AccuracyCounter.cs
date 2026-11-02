using System;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyCounter : MonoBehaviour
{
    [SerializeField] private Weapon _playerWeapon;

    private readonly List<Bullet> _bullets = new List<Bullet>();

    private int _hits;
    private int _completedShots;

    public event Action<float> Changed;

    public float Value => _completedShots == 0
        ? 0f
        : (float)_hits / _completedShots * 100f;

    private void OnEnable()
    {
        _playerWeapon.Shot += OnShot;
    }

    private void OnDisable()
    {
        _playerWeapon.Shot -= OnShot;

        foreach (Bullet bullet in _bullets)
            bullet.FlightFinished -= OnFlightFinished;

        _bullets.Clear();
    }

    private void OnShot(Bullet bullet)
    {
        _bullets.Add(bullet);
        bullet.FlightFinished += OnFlightFinished;
    }

    private void OnFlightFinished(Bullet bullet, bool hasHit)
    {
        bullet.FlightFinished -= OnFlightFinished;
        _bullets.Remove(bullet);

        _completedShots++;

        if (hasHit)
            _hits++;

        Changed?.Invoke(Value);
    }
}