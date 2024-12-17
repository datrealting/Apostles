using UnityEngine;

public class OrbitPlayer : MonoBehaviour
{
    public PlayerControl player;


    public Transform target; // Reference to the player
    public float orbitDistance = 5f; // Fixed distance from the player
    public float orbitSpeed = 50f; // Speed of the orbit in degrees per second
    private float orbitAcceleration = 1.005f; // Acceleration of the speed
    public float angularOffset = 0f; // Angular offset in degrees for multiple instances

    private Rigidbody2D rb;
    private float currentAngle; // Tracks the current angle of this orbiter

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize the current angle with the angular offset
        currentAngle = angularOffset;

        // Set the initial position based on the offset
        UpdatePosition();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        // Increment the current angle based on orbit speed and time
        currentAngle += orbitSpeed * Time.fixedDeltaTime;

        // Keep the angle within 0-360 degrees
        currentAngle %= 360f;

        // Update the position of the orbiter
        UpdatePosition();

        orbitDistance = orbitDistance - 0.01f;
        orbitSpeed = orbitSpeed * orbitAcceleration;

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

        if (player != null)
        {
            rb.MovePosition((Vector2)target.position + offset);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                other.GetComponent<NPCStats>()?.TakeDamage(1);  // Use the damage passed from the weapon
                GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(other.gameObject);
                player.TakeDamage(1);
                Destroy(gameObject);
        } 
    }
}