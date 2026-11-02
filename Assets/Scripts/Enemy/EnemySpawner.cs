using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _minY = -3f;
    [SerializeField] private float _maxY = 3f;
    [SerializeField, Min(0f)] private float _spawnOffset = 2f;

    [Header("Difficulty Scaling")]
    [SerializeField] private float _intervalDecreaseStep = 0.1f;
    [SerializeField] private float _minSpawnInterval = 0.5f;

    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioSource _effectsSource;

    private Coroutine _spawnCoroutine;
    private float _currentInterval;

    public event Action<Enemy> Spawned;

    private void OnEnable()
    {
        _currentInterval = _spawnInterval;
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_currentInterval);

            Spawn();

            _currentInterval = Mathf.Max(_minSpawnInterval, _currentInterval - _intervalDecreaseStep);
        }
    }

    private void Spawn()
    {
        float rightEdge = _camera.transform.position.x
            + _camera.orthographicSize * _camera.aspect;

        Vector3 position = new Vector3(
            rightEdge + _spawnOffset,
            UnityEngine.Random.Range(_minY, _maxY),
            0f);

        float leftEdge = _camera.transform.position.x
            - _camera.orthographicSize * _camera.aspect;

        Enemy enemy = Instantiate(
            _enemyPrefab, position, Quaternion.identity);

        enemy.Initialize(leftEdge, _effectsSource);
        Spawned?.Invoke(enemy);
    }
}