using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 10f;           // Speed of the projectile
    public float lifeTime = 5f;         // Time before the projectile is destroyed
    public GameObject impactEffect;     // Optional effect on impact

    private Vector3 direction;          // Direction the projectile will move

    void Start()
    {
        // Calculate direction towards the cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // Ensure we're working in 2D space

        direction = (mousePosition - transform.position).normalized;

        // Destroy the projectile after a set time
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the projectile in the calculated direction
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hits an enemy or wall
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            // Optional: Apply damage if hitting an enemy
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<NPCStats>()?.TakeDamage(10);  // Example damage value
            }

            // Optional: Instantiate an impact effect
            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            // Destroy the projectile on impact
            Console.WriteLine("Impacted!");
            Destroy(gameObject);
        }
    }
}
