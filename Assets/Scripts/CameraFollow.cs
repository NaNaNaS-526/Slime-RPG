using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x + 1, 3, -10);
    }
}