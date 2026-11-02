using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    public event Action ShootMomentReached;
    public event Action ExplosionFinished;
    public event Action WarningFinished;
    public event Action ExplosionMoment;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(AnimatorParameters.Attack);
    }

    public void OnShootMoment()
    {
        ShootMomentReached?.Invoke();
    }

    public void PlayExplosion()
    {
        _animator.ResetTrigger(AnimatorParameters.Attack);
        _animator.SetBool(AnimatorParameters.IsDead, true);
    }

    public void PlayDetected()
    {
        _animator.ResetTrigger(AnimatorParameters.Attack);
        _animator.SetBool(AnimatorParameters.IsDetected, true);
    }

    public void OnWarningFinished()
    {
        WarningFinished?.Invoke();
    }

    public void OnExplosionMoment()
    {
        ExplosionMoment?.Invoke();
    }

    public void OnExplosionFinished()
    {
        ExplosionFinished?.Invoke();
    }
}
