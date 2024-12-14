using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    public Transform currentPos;

    //temp
    public GameObject marker;

    private GameObject player;
    private float distance;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(distance);
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
                float maxradius = 7.4f;
                float minradius = 5f;
                Vector3 playerPos = player.gameObject.transform.position;

                // Generate a random point within the radius
                float randomX = Random.Range(-maxradius, maxradius);
                float randomY = Random.Range(-maxradius, maxradius);

                // Ensure the random point is within the minimum radius range as well
                float offsetX = Random.Range(-minradius, minradius);
                float offsetY = Random.Range(-minradius, minradius);

                // Generate the final position
                Vector3 randomPosition = new Vector3(playerPos.x + randomX + offsetX, playerPos.y + randomY + offsetY, currentPos.position.z);

                // Set the position of currentPos (or move the marker if that's the purpose)
                currentPos.position = randomPosition;

                // Optionally, instantiate the marker at the random position
                if (marker != null)
                {
                    Instantiate(marker, randomPosition, Quaternion.identity);
                }
            }
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
}
