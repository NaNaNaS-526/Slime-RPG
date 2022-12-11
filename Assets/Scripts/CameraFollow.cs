using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        var nextPosition = Vector3.Lerp(transform.position, playerTransform.position + offset, Time.deltaTime);
        transform.position = nextPosition;
    }
}