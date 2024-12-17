using UnityEngine;

public class OrbitPlayer : MonoBehaviour
{
    private PlayerControl player; // Reference to the PlayerControl script
    private Transform target;     // Reference to the player's Transform
    public float orbitDistance = 5f; // Fixed distance from the player
    public float orbitSpeed = 50f; // Speed of the orbit in degrees per second
    private float orbitAcceleration = 1.005f; // Acceleration of the speed
    public float angularOffset = 0f; // Angular offset in degrees for multiple instances

    private Rigidbody2D rb;
    private float currentAngle; // Tracks the current angle of this orbiter

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the player object and assign its components
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerControl>();
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
            return;
        }

        // Initialize the current angle with the angular offset
        currentAngle = angularOffset;

        // Set the initial position based on the offset
        UpdatePosition();
    }

    void FixedUpdate()
    {
        if (player == null || target == null)
            return;

        // Increment the current angle based on orbit speed and time
        currentAngle += orbitSpeed * Time.fixedDeltaTime;

        // Keep the angle within 0-360 degrees
        currentAngle %= 360f;

        // Update the position of the orbiter
        UpdatePosition();

        // Adjust orbit distance and speed over time
        orbitDistance -= 0.01f;
        orbitSpeed *= orbitAcceleration;

        // Destroy the orbiter if it gets too close
        if (orbitDistance <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdatePosition()
    {
        // Calculate the new position based on the current angle
        float angleRadians = currentAngle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * orbitDistance;

        // Update the orbiter's position relative to the player
        rb.MovePosition((Vector2)target.position + offset);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply damage and invoke events
            other.GetComponent<NPCStats>()?.TakeDamage(1);
            player?.TakeDamage(1);
            player?.onStrike?.Invoke(other.gameObject);

            Destroy(gameObject);
        }
    }
}
