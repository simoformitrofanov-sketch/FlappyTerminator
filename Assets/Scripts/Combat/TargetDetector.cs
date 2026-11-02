using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _radius = 2f;
    [SerializeField] private LayerMask _targetLayer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public bool IsTargetNearby()
    {
        return Physics2D.OverlapCircle(
            transform.position,
            _radius,
            _targetLayer) != null;
    }
}