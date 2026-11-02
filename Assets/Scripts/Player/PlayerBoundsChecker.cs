using UnityEngine;

public class PlayerBoundsChecker : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public bool IsOutside()
    {
        float viewportY = _camera.WorldToViewportPoint(
            transform.position).y;

        return viewportY < 0f || viewportY > 1f;
    }
}