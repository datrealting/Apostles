using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public Transform currentPos;
    public float minTeleportRadius = 3f; // Minimum radius around the player
    public float maxTeleportRadius = 7f; // Maximum radius around the player

    private GameObject player;
    private float distance;
    private float timer;

    // Start is called before the first execution of Update
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
                // Generate a random angle
                float angle = Random.Range(0f, 2f * Mathf.PI);

                // Generate a random distance within the min and max radius
                float radius = Random.Range(minTeleportRadius, maxTeleportRadius);

                // Calculate the random position
                Vector3 randomPosition = new Vector3(
                    player.transform.position.x + Mathf.Cos(angle) * radius,
                    player.transform.position.y + Mathf.Sin(angle) * radius,
                    currentPos.position.z // Keep Z unchanged for 2D
                );

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
