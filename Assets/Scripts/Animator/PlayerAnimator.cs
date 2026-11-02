using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayJump()
    {
        _animator.SetTrigger(AnimatorParameters.Jump);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(AnimatorParameters.Attack);
    }
}