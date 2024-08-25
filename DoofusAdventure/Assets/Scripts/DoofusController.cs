using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 3f;       // Speed of Doofus
    public float smoothTime = 0.1f; // Time to smooth out the velocity
    private Rigidbody rb;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();

        // Debug log to check initial speed value
        Debug.Log($"Initial Speed: {speed}");
    }

    void FixedUpdate()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a target velocity based on input
        Vector3 targetVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;

        // Smoothly interpolate between the current velocity and the target velocity
        Vector3 smoothVelocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);

        // Set the Rigidbody's velocity
        rb.velocity = smoothVelocity;

        // Debug log to check speed value during runtime
        Debug.Log($"Speed: {speed}, Velocity: {rb.velocity}");
    }
}
