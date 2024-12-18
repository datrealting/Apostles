using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour, IAoe
{
    public GameObject telegraphPos;   // Visual indicator for projectile (telegraph)
    public GameObject projectile;     // The projectile to spawn
    public Transform currentPos;      // Current position to keep Z-axis consistent
    public float spawnRate = 0.7f;    // Time between telegraph spawns

    // Reference to the player
    private GameObject target;
    private float distance;
    private float timer;
    private float nextTime;

    // AOE
    public GameObject aoePrefab;
    public float attackCD = 3f;
    public float attackSize = 3f;
    public float attackRange = 1.5f;
    public float attackDelay = 5f;

    // Public Collider2D to assign via the inspector
    public Collider2D spawnAreaCollider;

    // Orbiter Enemy Spawning
    public GameObject orbiterEnemyParentPrefab; // Prefab to spawn
    private bool hasSpawnedOrbiter = false; // Ensure it spawns only once

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        getDistanceToTarget();
        rangedAttack();
        meleeAttack();
        CheckHealthForSpawn();
    }

    void rangedAttack()
    {
        if (distance > 5 && spawnAreaCollider != null)
        {
            Bounds b = spawnAreaCollider.bounds;

            timer += Time.deltaTime;

            if (timer > spawnRate) // Control telegraph spawning
            {
                timer = 0;

                // Generate a random point within the collider's bounds
                Vector3 randomPosition = new Vector3(
                    Random.Range(b.min.x, b.max.x),
                    Random.Range(b.min.y, b.max.y),
                    currentPos.position.z // Keep the Z position unchanged for 2D
                );

                // Use ClosestPoint to ensure the random point is valid within the collider
                randomPosition = spawnAreaCollider.ClosestPoint(randomPosition);

                // Spawn the telegraph at the random position
                GameObject spawnTelegraph = Instantiate(telegraphPos, randomPosition, Quaternion.identity);

                // Start the coroutine to handle projectile timing
                StartCoroutine(SpawnProjectileAfterDelay(randomPosition, 1f)); // Spawn projectile after 1 second
            }
        }
    }

    void meleeAttack()
    {
        if (Time.time < nextTime)
            return; // Exit if attack is on cooldown

        if (distance < 5f)
        {
            Swipe();
            nextTime = Time.time + attackCD; // Set the cooldown
        }
    }

    IEnumerator SpawnProjectileAfterDelay(Vector3 spawnPosition, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 1 second

        // Spawn the projectile at the same position as the telegraph
        GameObject newProjectile = Instantiate(projectile, spawnPosition, Quaternion.identity);
    }

    void getDistanceToTarget()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
    }

    void Swipe()
    {
        // Use the range variable to dynamically adjust the offset
        Vector2 attackPosition = (Vector2)transform.position + (GetVectorToTarget() * attackRange);

        // Spawn the AOE at the adjusted position
        AOEController.SpawnAOE(this, aoePrefab, attackPosition, attackSize, attackDelay, 0);
    }

    void SwipeEffect(Collider2D[] hits)
    {
        foreach (Collider2D obj in hits)
        {
            if (obj == null) continue;
            PlayerControl stats = obj.GetComponent<PlayerControl>();
            if (stats != null)
            {
                Debug.Log("I hit the player!");
                stats.TakeDamage(1);
            }
        }
    }

    public void AOEEffect(Collider2D[] hits)
    {
        SwipeEffect(hits);
    }

    private Vector2 GetVectorToTarget()
    {
        var heading = target.transform.position - transform.position;
        var direction = heading.normalized;
        return direction;
    }

    // Check if the boss's health has reached 50% or less
    void CheckHealthForSpawn()
    {
        NPCStats stats = GetComponent<NPCStats>(); // Get NPCStats component

        if (stats != null && !hasSpawnedOrbiter)
        {
            if (stats.currenthp <= stats.maxhp * 0.5f) // Check if health is 50% or less
            {
                SpawnOrbiter();
                hasSpawnedOrbiter = true; // Prevent multiple spawns
            }
        }
    }

    // Spawn the OrbiterEnemyParent prefab
    void SpawnOrbiter()
    {
        if (orbiterEnemyParentPrefab != null)
        {
            Instantiate(orbiterEnemyParentPrefab, transform.position, Quaternion.identity); // Spawn at the boss's position
        }
        else
        {
            Debug.LogWarning("OrbiterEnemyParentPrefab is not assigned in the Inspector!");
        }
    }
}
