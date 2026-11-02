using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAim : MonoBehaviour
{
    [SerializeField] private float _minAngle = -60f;
    [SerializeField] private float _maxAngle = 30f;
    [SerializeField] private float _anglePerSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 180f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Rotate(float verticalSpeed)
    {
        float targetAngle = Mathf.Clamp(
            verticalSpeed * _anglePerSpeed,
            _minAngle,
            _maxAngle);

        float nextAngle = Mathf.MoveTowardsAngle(
            _rigidbody.rotation,
            targetAngle,
            _rotationSpeed * Time.fixedDeltaTime);

        _rigidbody.MoveRotation(nextAngle);
    }
}