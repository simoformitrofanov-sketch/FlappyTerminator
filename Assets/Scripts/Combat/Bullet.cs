using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField, Min(1)] private int _damage = 1;

    private Rigidbody2D _rigidbody;
    private LayerMask _targetLayers;
    private bool _hasHit;

    public event Action<Bullet, bool> FlightFinished;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit)
            return;

        if ((_targetLayers.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (other.TryGetComponent(out IDamageable target) == false)
            return;

        _hasHit = true;
        FlightFinished?.Invoke(this, true);

        target.TakeDamage(_damage);
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, LayerMask targetLayers)
    {
        _targetLayers = targetLayers;
        _rigidbody.velocity = direction.normalized * _speed;

        StartCoroutine(WaitForLifetime());
    }

    private IEnumerator WaitForLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);

        if (_hasHit)
            yield break;

        FlightFinished?.Invoke(this, false);
        Destroy(gameObject);
    }
}