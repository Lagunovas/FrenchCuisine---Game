using UnityEngine;

public class Camera2DFollowTarget : MonoBehaviour {

    public Transform target;

    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector3 offset;

    private void LateUpdate() {
       Vector3 desiredPosition = target.position + offset;
       Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
       transform.position = smoothedPosition;
    }

}