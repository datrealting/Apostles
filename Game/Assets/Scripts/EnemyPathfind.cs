using UnityEngine;

public class EnemyPathfind : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float agroDistance;
    public LayerMask obstacleLayer;

    private float distance;

    void FixedUpdate()
    {
        if (HasLineOfSight())
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    bool HasLineOfSight()
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;

        // Perform the raycast with obstacleLayer mask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, agroDistance, obstacleLayer);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == player)
            {
                Debug.Log("Line of sight to player detected.");
                return true;
            }
            else
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing, clear line to player.");
            return true;
        }

        Debug.Log("No line of sight to player.");
        return false;
    }
}
