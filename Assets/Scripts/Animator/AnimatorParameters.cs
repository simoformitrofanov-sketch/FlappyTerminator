using UnityEngine;

public static class AnimatorParameters
{
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int IsDead = Animator.StringToHash("IsDead");
    public static readonly int IsDetected = Animator.StringToHash("IsDetected");
}
