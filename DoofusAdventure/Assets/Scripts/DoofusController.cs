using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 5f;
    public float smoothTime = 0.1f;
    public float raycastDistance = 1.1f; 
    public LayerMask pulpitLayer; 

    private Rigidbody rb;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Debug.Log($"Initial Speed: {speed}");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;

       
        Vector3 smoothVelocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
        rb.velocity = new Vector3(smoothVelocity.x, rb.velocity.y, smoothVelocity.z);

        if (!IsOnPulpit())
        {
            rb.AddForce(Vector3.down, ForceMode.Acceleration);
        }

        Debug.Log($"Speed: {speed}, Velocity: {rb.velocity}");
    }

    bool IsOnPulpit()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, pulpitLayer);
    }
}