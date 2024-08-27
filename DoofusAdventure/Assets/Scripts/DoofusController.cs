using UnityEngine;
using UnityEngine.SceneManagement;

public class DoofusController : MonoBehaviour
{
    public float speed = 5f;
    public float smoothTime = 0.1f;
    public float raycastDistance = 1.1f;
    public LayerMask pulpitLayer;
    public float fallThreshold = 90; // Y position below which the game ends

    private Rigidbody rb;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isGameOver = false;

    public float initialHeight = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        transform.position = new Vector3(0, initialHeight + 1f, 0);
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;

        Vector3 smoothVelocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
        rb.velocity = new Vector3(smoothVelocity.x, rb.velocity.y, smoothVelocity.z);

        if (!IsOnPulpit())
        {
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

        // Check if Doofus has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            GameOver();
        }
    }

    bool IsOnPulpit()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, pulpitLayer);
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        // You can add more game over logic here, like showing a UI panel
        // For now, we'll just reload the scene after a short delay
        Invoke("ReloadScene", 0.1f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnNewPulpitReached()
    {
        // This method will be called by the PulpitManager when a new pulpit is reached
        Debug.Log("New pulpit reached!");
        ScoreManager.Instance.IncrementPulpitCount();
    }
}