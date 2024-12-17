using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public Transform currentPos;
    public Collider2D Bounds;

    private GameObject player;
    private float distance;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 7.5f)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }

        Relocate();
    }

    void Relocate()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            timer = 0;

            if (distance > 7.5f)
            {
                // Use the bounds of the assigned collider to restrict relocation
                Bounds b = Bounds.bounds;

                // Generate a random point within the bounds
                Vector3 randomPosition = new Vector3(
                    Random.Range(b.min.x, b.max.x),
                    Random.Range(b.min.y, b.max.y),
                    currentPos.position.z // Keep the Z position unchanged for 2D
                );

                // Use ClosestPoint to ensure the random point is valid within the collider
                randomPosition = Bounds.ClosestPoint(randomPosition);

                // Set the new position
                currentPos.position = randomPosition;
            }
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
}
