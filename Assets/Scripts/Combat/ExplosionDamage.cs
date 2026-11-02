using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _radius = 1.5f;
    [SerializeField, Min(1)] private int _damage = 1;
    [SerializeField] private LayerMask _targetLayers;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void ApplyDamage()
    {
        Collider2D target = Physics2D.OverlapCircle(
            transform.position,
            _radius,
            _targetLayers);

        if (target == null)
            return;

        if (target.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);
    }
}