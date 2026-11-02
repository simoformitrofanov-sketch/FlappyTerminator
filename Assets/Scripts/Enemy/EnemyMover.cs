using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField, Min(0f)] private float _amplitude = 0.5f;
    [SerializeField, Min(0.1f)] private float _oscillationPeriod = 2f;

    private Rigidbody2D _rigidbody;
    private float _startY;
    private float _elapsedTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _startY = _rigidbody.position.y;
        _elapsedTime = 0f;
    }

    public void Move()
    {
        _elapsedTime += Time.fixedDeltaTime;

        float phase = _elapsedTime / _oscillationPeriod * Mathf.PI * 2f;

        Vector2 nextPosition = new Vector2(
            _rigidbody.position.x - _speed * Time.fixedDeltaTime,
            _startY + Mathf.Sin(phase) * _amplitude);

        _rigidbody.MovePosition(nextPosition);
    }
}