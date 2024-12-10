using System.Collections;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile
    [SerializeField] private Transform projectileSpawnPoint; // Spawn point for the projectile
    [SerializeField] private GameObject impactEffect; // Impact effect prefab
    [SerializeField] private float weaponDamage = 1f; // Damage multiplier for the weapon as a percentage
    [SerializeField] private float weaponAtkspeed = 0.0001f; // ATKSpeed multiplier for the weapon as a percentage. Higher numbers means higher atk speed
    private bool canAttack = true;
    [SerializeField] private float projectileSpeed = 20f; // Speed of the projectile
    [SerializeField] private float projectileRange = 10f; // Range of the projectile

    private Transform playerTransform; // Reference to the player
    private Transform weaponPosition; // Reference to the weapon holder
    private Vector3 localOffset; // Local offset to keep the weapon attached to the player

    private PlayerControl playerControlReference;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerControlReference = GameObject.Find("Player").GetComponent<PlayerControl>();
        weaponPosition = GameObject.FindGameObjectWithTag("weaponHolder").transform;

        // Calculate the initial offset from the player's position
        localOffset = transform.localPosition;
    }

    void Update()
    {
        RotateWeaponTowardCursor(); // Rotate the weapon toward the cursor
        HandleAttackInput();    // Handle attack input
    }

    public void Attack()
    {
        // Instantiate a new projectile at the spawn point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Assign the impact effect and damage to the projectile's impact behavior
        ProjectileImpact impactScript = projectile.GetComponent<ProjectileImpact>();
        if (impactScript != null && playerControlReference != null)
        {
            impactScript.Setup(impactEffect, playerControlReference.GetActualDamage(weaponDamage)); // Pass damage to the impact script
        }

        // Calculate the direction to the cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        mousePosition.z = transform.position.z; // Ensure it's in the same z-plane (NOT INVISIBLE)
        Vector2 direction = (mousePosition - projectileSpawnPoint.position).normalized;

        // Rotate the projectile to face the direction of the cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Add projectile movement
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;

            // Add range logic
            Destroy(projectile, projectileRange / projectileSpeed); // Destroy the projectile after reaching its range
        }
    }





    private void HandleAttackInput()
    {
        // Check if the player clicks the left mouse button
        if (canAttack)
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                Debug.Log(1 / (playerControlReference.GetAtkSpeed(weaponAtkspeed)));
                Attack(); // Trigger the attack
                canAttack = false;
                StartCoroutine(WeaponCD());
            }
        }
    }
    private IEnumerator WeaponCD()
    {
        yield return new WaitForSeconds(1 / (playerControlReference.GetAtkSpeed(weaponAtkspeed)));
        canAttack = true;
    }

    private void RotateWeaponTowardCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        mousePosition.z = transform.position.z; // Make sure it stays in the same z-plane (not invisible lol)

        // Calculate direction from weapon to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if the player is flipped (facing left)
        if (playerTransform.localScale.x < 0)
        {
            // Adjust the angle for the flipped orientation
            angle += 180f; // Mirror the weapon's rotation when flipped
        }

        // Apply rotation
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the weapon's sprite if necessary
        bool shouldFlipWeaponSprite = playerTransform.localScale.x < 0;
        GetComponent<SpriteRenderer>().flipY = shouldFlipWeaponSprite;
    }

}
