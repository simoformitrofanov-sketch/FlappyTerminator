using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerBoundsChecker))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField, Min(0.01f)] private float _shootInterval = 0.3f;
    [SerializeField] private AudioSource _effectsSource;

    private PlayerInput _input;
    private PlayerMover _mover;
    private PlayerAim _aim;
    private Weapon _weapon;
    private Health _health;
    private PlayerBoundsChecker _boundsChecker;
    private PlayerAnimator _animator;
    private float _nextShootTime;

    public event Action Died;
    public event Action PauseRequested;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _mover = GetComponent<PlayerMover>();
        _aim = GetComponent<PlayerAim>();
        _weapon = GetComponent<Weapon>();
        _health = GetComponent<Health>();
        _boundsChecker = GetComponent<PlayerBoundsChecker>();
        _animator = GetComponent<PlayerAnimator>();
        _weapon.Initialize(_effectsSource);
    }

    private void OnEnable()
    {
        _input.JumpRequested += OnJumpRequested;
        _input.ShootRequested += OnShootRequested;
        _input.PauseRequested += OnPauseRequested;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _input.JumpRequested -= OnJumpRequested;
        _input.ShootRequested -= OnShootRequested;
        _input.PauseRequested -= OnPauseRequested;
        _health.Died -= OnDied;
    }

    private void Update()
    {
        if (_input.enabled)
            _input.Read();

        if (Time.timeScale == 0f)
            return;

        if (_boundsChecker.IsOutside())
            Die();
    }

    private void FixedUpdate()
    {
        if (_aim.enabled)
            _aim.Rotate(_mover.VerticalSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_health.IsDead || other.enabled == false)
            return;

        if (other.TryGetComponent(out Enemy _))
            Die();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void Die()
    {
        _health.TakeDamage(_health.Current);
    }

    private void OnJumpRequested()
    {
        _mover.Jump();
        _animator.PlayJump();
    }

    private void OnShootRequested()
    {
        if (Time.timeScale == 0f || Time.time < _nextShootTime)
            return;

        _nextShootTime = Time.time + _shootInterval;

        _weapon.Shoot();
        _animator.PlayAttack();
    }

    private void OnPauseRequested()
    {
        PauseRequested?.Invoke();
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
        Died?.Invoke();
    }
}