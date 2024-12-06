using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile
    [SerializeField] private Transform projectileSpawnPoint; // Spawn point for the projectile
    [SerializeField] private GameObject impactEffect; // Impact effect prefab
    [SerializeField] private int projectileDamage = 10; // Damage dealt by the projectile
    [SerializeField] private float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] private float projectileRange = 10f; // Range of the projectile

    private Transform playerTransform; // Reference to the player
    private Transform weaponPosition; // Reference to the weapon holder
    private Vector3 localOffset; // Local offset to keep the weapon attached to the player

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        weaponPosition = GameObject.FindGameObjectWithTag("weaponHolder").transform;

        // Calculate the initial offset from the player's position
        localOffset = transform.localPosition;
    }

    void Update()
    {
        AdjustWeaponPosition(); // Keep weapon attached to the player
        RotateWeaponTowardCursor(); // Rotate the weapon toward the cursor
        HandleAttackInput();    // Handle attack input
    }

    public void Attack()
    {
        // Instantiate a new projectile at the spawn point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Assign the impact effect and damage to the projectile's impact behavior
        ProjectileImpact impactScript = projectile.GetComponent<ProjectileImpact>();
        if (impactScript != null)
        {
            impactScript.Setup(impactEffect, projectileDamage); // Pass damage to the impact script
        }

        // Add projectile movement (direction and speed)
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Get the forward direction of the weapon using its rotation
            Vector2 direction = transform.right;  // Use 'right' instead of 'up' for forward direction

            // Apply velocity to move the projectile forward
            rb.linearVelocity = direction * projectileSpeed; // Apply speed to the projectile

            // Add range logic
            Destroy(projectile, projectileRange / projectileSpeed); // Destroy the projectile after reaching its range
        }
    }



    private void HandleAttackInput()
    {
        // Check if the player clicks the left mouse button
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Attack(); // Trigger the attack
        }
    }

    private void AdjustWeaponPosition()
    {
        // Attach the weapon to the player but keep its local rotation unaffected by the player's flip
        transform.position = weaponPosition.position + localOffset;
    }

    private void RotateWeaponTowardCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        mousePosition.z = transform.position.z; // Make sure it stays in the same z-plane

        // Calculate direction from weapon to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
