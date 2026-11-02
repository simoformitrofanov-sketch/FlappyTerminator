using System;
using UnityEngine;
using System.Collections.Generic;

public class StreakCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;

    private readonly List<Enemy> _enemies = new List<Enemy>();

    public event Action<int> Changed;

    public int Value { get; private set; }

    private void OnEnable()
    {
        foreach (EnemySpawner spawner in _spawners)
            spawner.Spawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        foreach (EnemySpawner spawner in _spawners)
            spawner.Spawned -= OnEnemySpawned;

        foreach (Enemy enemy in _enemies)
        {
            enemy.Exploded -= OnEnemyExploded;
            enemy.Escaped -= OnEnemyEscaped;
        }

        _enemies.Clear();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _enemies.Add(enemy);

        enemy.Exploded += OnEnemyExploded;
        enemy.Escaped += OnEnemyEscaped;
    }

    private void OnEnemyExploded(Enemy enemy)
    {
        StopTracking(enemy);
        Add();
    }

    private void OnEnemyEscaped(Enemy enemy)
    {
        StopTracking(enemy);
        ResetStreak();
    }

    private void StopTracking(Enemy enemy)
    {
        enemy.Exploded -= OnEnemyExploded;
        enemy.Escaped -= OnEnemyEscaped;

        _enemies.Remove(enemy);
    }

    private void Add()
    {
        Value++;
        Changed?.Invoke(Value);
    }

    private void ResetStreak()
    {
        if (Value == 0)
            return;

        Value = 0;
        Changed?.Invoke(Value);
    }
}