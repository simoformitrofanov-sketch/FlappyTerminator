using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _first;
    [SerializeField] private SpriteRenderer _second;
    [SerializeField, Min(0f)] private float _speed = 0.15f;

    private float _width;

    private void Start()
    {
        _width = _first.bounds.size.x;

        _second.transform.position =
            _first.transform.position + Vector3.right * _width;
    }

    private void Update()
    {
        Vector3 offset = Vector3.left * _speed * Time.deltaTime;

        _first.transform.position += offset;

        _second.transform.position = new Vector3(
            _first.transform.position.x + _width,
            _first.transform.position.y,
            _first.transform.position.z);

        float leftEdge = _camera.transform.position.x
            - _camera.orthographicSize * _camera.aspect;

        if (_first.bounds.max.x < leftEdge)
        {
            SpriteRenderer previousFirst = _first;
            _first = _second;
            _second = previousFirst;

            _second.transform.position =
                _first.transform.position + Vector3.right * _width;
        }
    }
}