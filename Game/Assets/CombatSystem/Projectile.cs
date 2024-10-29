using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    // Method to set the damage value
    public void Initialize(float damageValue)
    {
        damage = damageValue;
    }



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);

        // Check if the collided object has an NPCStats component
        NPCStats npcStats = other.GetComponent<NPCStats>();
        if (npcStats != null)
        {
            // Call the TakeDamage method on the NPCStats component
            npcStats.TakeDamage(damage);
        }


        
        // Destroy the projectile
        Destroy(gameObject);   
    }

    

}
