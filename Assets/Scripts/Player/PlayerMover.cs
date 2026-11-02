using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpSpeed = 5f;

    private Rigidbody2D _rigidbody;

    public float VerticalSpeed => _rigidbody.velocity.y;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(
            _rigidbody.velocity.x,
            _jumpSpeed);
    }
}