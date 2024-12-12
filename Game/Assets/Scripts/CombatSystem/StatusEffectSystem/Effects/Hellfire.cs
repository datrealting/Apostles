using System.Collections;
using System.Runtime;
using UnityEngine;

public class Hellfire : BaseSE
{
    public override OverrideType overrideType => OverrideType.Stack;

    public override float duration { get; set; } = 2f;  // Settable in this class
    public override float tickFrequency => 0.1f;

    public float damagePerTick = 2f;
    public float searchRadius = 200f;

    private GameObject nextNPC;
    private BaseSE nextEffect = null;
    private GameObject spritePrefab = null;


    void Awake()
    {
        nextEffect = new Hellfire();
        spritePrefab = Resources.Load<GameObject>("HellfirePrefab");
    }
    public override void OnApply()
    {
        Debug.Log("Additional logic for BURNING application!");
        sprite = Instantiate(spritePrefab, target.transform);
    }
    public override void OnTick()
    {
        if (targetStats != null)
        {
            targetStats.TakeDamage(damagePerTick);
        }
        if (targetStats == null)
        {
            Debug.Log("Something died with BURNING on");
        }
    }
    public override void OnExpire()
    {
        Debug.Log("Additional logic for BURNING running out!");
    }
    public override void OnDie()
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
            StatusEffectManager.ApplyEffect(nextNPC, caller, nextEffect, spritePrefab);
        }
        else
        {
            Debug.Log("No NPCs found within the radius.");
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the search radius in the editor for debugging
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}  
