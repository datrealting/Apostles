using System.Collections;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

// LIGHTNING BOLT
/*
 * 1. Strikes target with lightning, dealing high damage and potentially spawning a tracer that chases another target.
 * 2. The tracer bounces to up to bounceCount targets, dealing low damage but each time having a chance to spawn another Lightning Bolt effect.
 * 3. This has potential to cause ridiculously huge chains of lightning bolts and tracers.
 */


public class LightningBolt : BaseSE
{
    public override OverrideType overrideType => OverrideType.Stack;

    public override float duration { get; set; } = 0.4f;  // Settable in this class
    public override float tickFrequency => 0.5f;

    public float damage = 50f;
    public float boltDamage = 10f;
    public int bounceCount = 3;
    public GameObject nextNPC;
    private BaseSE nextEffect = null;
    private GameObject spritePrefab = null;
    public float searchRadius = 500f;
    void Awake()
    {
        nextEffect = new LightningBolt();
        spritePrefab = Resources.Load<GameObject>("LightningBoltPrefab");
    }
    public override void OnApply()
    {
        sprite = Instantiate(spritePrefab, target.transform);
        targetStats.TakeDamage(damage);
    }
    public override void OnTick()
    {

    }
    public override void OnExpire()
    {
        // Find all colliders within the search radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        float closestDistance = Mathf.Infinity; // To track the closest NPC
        nextNPC = null; // Reset the closest NPC

        foreach (var hitCollider in hitColliders)
        {
            // Check if the object is not itself
            if (hitCollider.gameObject == gameObject)
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
                    nextNPC = hitCollider.gameObject; // Assign closest NPC
                }
            }
        }

        if (nextNPC != null)
        {
            Debug.Log("Closest NPC assigned: " + nextNPC.name);
            GameObject bolt = Instantiate(Resources.Load<GameObject>("PROJECTILELightningBolt"), transform.position, Quaternion.identity);
            bolt.GetComponent<BoltMovement>().nextNPC = nextNPC;
            bolt.GetComponent<BoltMovement>().damage = 10f;
            bolt.GetComponent<BoltMovement>().caller = gameObject;
            bolt.GetComponent<BoltMovement>().prevNPC = gameObject;
            bolt.GetComponent<BoltMovement>().nextEffect = nextEffect;
            bolt.GetComponent<BoltMovement>().spritePrefab = spritePrefab;
        }
        else
        {
            Debug.Log("No NPCs found within the radius.");
        }
    }
    public override void OnDie()
    {

    }
}
