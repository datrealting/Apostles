using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoltMovement : MonoBehaviour
{
    public GameObject nextNPC;
    public GameObject prevNPC;

    public BaseSE nextEffect;
    public GameObject spritePrefab;
    public GameObject caller;

    public float speed = 100f;
    public float damage = 0f;
    private int currentBounce = 0;
    public int bounces = 3;
    public int strikeChance = 70;
    public float searchRadius = 500f;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextNPC.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.gameObject != prevNPC)
        {
            other.GetComponent<NPCStats>()?.TakeDamage(damage);  // Use the damage passed from the bolt
            if (LightningStrikesAgain())
            {
                StatusEffectManager.ApplyEffect(other.gameObject, caller, nextEffect, spritePrefab);
            }
            prevNPC = nextNPC;
            // Find all colliders within the search radius
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

            float closestDistance = Mathf.Infinity; // To track the closest NPC
            GameObject foundNPC = null; // Reset the closest NPC

            foreach (var hitCollider in hitColliders)
            {
                // Check if the object is not itself
                if (hitCollider.gameObject == prevNPC)
                {
                    continue; // Skip this iteration
                }
                // Check if the object has an NPCStats component
                NPCStats npcStats = hitCollider.GetComponent<NPCStats>();
                if (npcStats != null)
                {
                    // Calculate the distance to this NPC
                    float distance = Vector2.Distance(transform.position, hitCollider.transform.position);

                    // Check if this NPC is the closest one so far
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        foundNPC = hitCollider.gameObject; // Assign closest NPC
                    }
                }
            }

            if (foundNPC != null)
            {
                nextNPC = foundNPC;
            }
            else
            {
                Debug.Log("No NPCs found within the radius.");
            }
            currentBounce++;
        } 
        if (currentBounce >= bounces)
        {
            Destroy(gameObject);
        }
    }

    bool LightningStrikesAgain()
    {
        return strikeChance <= Random.Range(1, 100);
    }
}
