using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 20f;           // Speed of the projectile
    public float range = 10f;         // Time before the projectile is destroyed
    public GameObject impactEffect;     // Optional effect on impact

    private Vector3 direction;          // Direction the projectile will move

    void Start()
    {
        InitializeDirection();           // Calculate direction
        ScheduleDestruction();           // Schedule to destroy after range
    }

    void Update()
    {
        MoveProjectile();                // Handle movement
    }

    // Calculate direction towards the cursor
    private void InitializeDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        // Use the current z position of the projectile to avoid moving behind the camera
        mousePosition.z = transform.position.z;  // Keep the z value consistent with the projectile's initial position

        direction = (mousePosition - transform.position).normalized;
    }

    // Schedule to destroy projectile after a set time
    private void ScheduleDestruction()
    {
        Destroy(gameObject, range);
    }

    // Move the projectile in the calculated direction at a constant speed
    private void MoveProjectile()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hits an enemy or wall
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            HandleCollision(other);  // Handle collision logic
        }
    }

    // Handle collision effects
    private void HandleCollision(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<NPCStats>()?.TakeDamage(10);  // Example damage value
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        Console.WriteLine("Impacted!");
        Destroy(gameObject);  // Destroy the projectile on impact
    }
}
