using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int _max = 1;

    public event Action Died;

    public int Current { get; private set; }
    public bool IsDead => Current == 0;

    private void Awake()
    {
        Current = _max;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0)
            return;

        Current = Mathf.Max(0, Current - damage);

        if (IsDead)
            Died?.Invoke();
    }
}