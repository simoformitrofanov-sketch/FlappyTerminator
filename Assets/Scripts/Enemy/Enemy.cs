using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(TargetDetector))]
[RequireComponent(typeof(ExplosionDamage))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float _shootDelay = 1.5f;
    [SerializeField, Range(0f, 1f)] private float _detectedVolume = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _explosionVolume = 0.7f;

    [Header("References")]
    [SerializeField] private AudioClip _detectedSound;
    [SerializeField] private AudioClip _explosionSound;

    private Weapon _weapon;
    private Health _health;
    private Collider2D _collider;
    private EnemyMover _mover;
    private EnemyAnimator _animator;
    private TargetDetector _targetDetector;
    private ExplosionDamage _explosionDamage;
    private Coroutine _shootingCoroutine;
    private AudioSource _effectsSource;
    private float _despawnX;
    private bool _isDetected;

    public event Action<Enemy> Exploded;
    public event Action<Enemy> Escaped;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        _health = GetComponent<Health>();
        _collider = GetComponent<Collider2D>();
        _mover = GetComponent<EnemyMover>();
        _animator = GetComponent<EnemyAnimator>();
        _targetDetector = GetComponent<TargetDetector>();
        _explosionDamage = GetComponent<ExplosionDamage>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        _animator.ShootMomentReached += OnShootMomentReached;
        _animator.ExplosionMoment += OnExplosionMomentReached;
        _animator.WarningFinished += OnWarningFinished;
        _animator.ExplosionFinished += OnExplosionFinished;
        _shootingCoroutine = StartCoroutine(ShootRepeatedly());
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        _animator.ShootMomentReached -= OnShootMomentReached;
        _animator.ExplosionMoment -= OnExplosionMomentReached;
        _animator.ExplosionFinished -= OnExplosionFinished;
        _animator.WarningFinished -= OnWarningFinished;
        StopShooting();
    }

    private void Update()
    {
        if (_health.IsDead)
            return;

        if (_collider.bounds.max.x < _despawnX)
        {
            Escaped?.Invoke(this);
            Destroy(gameObject);
            return;
        }

        if (_isDetected)
            return;

        if (_targetDetector.IsTargetNearby())
        {
            _isDetected = true;
            StopShooting();
            _animator.PlayDetected();
            _effectsSource.PlayOneShot(_detectedSound, _detectedVolume);
        }
    }

    private void FixedUpdate()
    {
        if (_mover.enabled)
            _mover.Move();
    }

    public void Initialize(float despawnX, AudioSource effectsSource)
    {
        _despawnX = despawnX;
        _effectsSource = effectsSource;

        _weapon.Initialize(effectsSource);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private IEnumerator ShootRepeatedly()
    {
        WaitForSeconds delay = new WaitForSeconds(_shootDelay);

        while (enabled)
        {
            yield return delay;

            _animator.PlayAttack();
        }
    }

    private void OnShootMomentReached()
    {
        if (_health.IsDead || _isDetected)
            return;

        _weapon.Shoot();
    }

    private void StopShooting()
    {
        if (_shootingCoroutine == null)
            return;

        StopCoroutine(_shootingCoroutine);
        _shootingCoroutine = null;
    }

    private void OnWarningFinished()
    {
        if (_health.IsDead)
            return;

        _health.TakeDamage(_health.Current);
    }

    private void OnDied()
    {
        StopShooting();
        _collider.enabled = false;
        _mover.enabled = false;
        _animator.PlayExplosion();
    }

    private void OnExplosionMomentReached()
    {
        _effectsSource.PlayOneShot(_explosionSound, _explosionVolume);

        Exploded?.Invoke(this);
        _explosionDamage.ApplyDamage();
    }

    private void OnExplosionFinished()
    {
        Destroy(gameObject);
    }
}