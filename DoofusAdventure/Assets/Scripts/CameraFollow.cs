using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform doofus;  // Reference to Doofus's transform
    public float smoothTime = 0.3f;
    public float height = 5f;  // Height of the camera above the cube
    public float distance = 12f;  // Distance behind the cube
    public float angle = 30f;  // Angle of the camera (in degrees)

    private Vector3 _currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (doofus == null) return;

        // Calculate the desired position of the camera
        Vector3 targetPosition = doofus.position;
        targetPosition -= Quaternion.Euler(angle, 0, 0) * Vector3.forward * distance;
        targetPosition += Vector3.up * height;

        // Smoothly move the camera towards that position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        // Make the camera look at the cube
        transform.LookAt(doofus);
    }
}