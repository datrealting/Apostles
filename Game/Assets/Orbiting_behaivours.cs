using Unity.VisualScripting;
using UnityEngine;

public class Orbiting_behaivours : MonoBehaviour
{
    public GameObject target; 
    private float damage = 10;
    public float orbitSpeed = 50f * GameObject.Find("Player").GetComponent<PlayerControl>().relicAtkspeedMult;
    private Vector2 orbitPosition;
    private Rigidbody2D rb;

    public Transform player; // Reference to the player
    public float orbitDistance = 5f; // Fixed distance from the player
    // Update is called once per frame
    void Start (){
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        // Set the initial position at the specified distance away from the player
        if (player != null)
        {
            Vector2 offset = new Vector2(orbitDistance, 0);
            transform.position = (Vector2)player.position + offset;
        }
    }

    void FixedUpdate()
    {
        // Calculate the current angle of rotation around the player
        Vector2 direction = (Vector2)transform.position - (Vector2)target.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Increment the angle based on the orbit speed
        angle -= orbitSpeed * Time.fixedDeltaTime;

        // Calculate the new position based on the updated angle
        float angleRadians = angle * Mathf.Deg2Rad;
        orbitPosition = (Vector2)target.transform.position + new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * orbitDistance;

        // Update the Rigidbody position to maintain physics consistency
        rb.MovePosition(orbitPosition);
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<NPCStats>()?.TakeDamage(damage);  // Use the damage passed from the weapon
                GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(other.gameObject);
            }
        }
    }
    
}
