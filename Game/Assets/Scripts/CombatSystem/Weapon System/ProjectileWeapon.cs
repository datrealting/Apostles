using System.Collections;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile
    [SerializeField] private Transform projectileSpawnPoint; // Spawn point for the projectile
    [SerializeField] private GameObject impactEffect; // Impact effect prefab

    protected override void Update()
    {
        base.Update();
        //RotateWeaponTowardCursor(); // Rotate the weapon toward the cursor
        HandleAttackInput();    // Handle attack input
        HandleLevelUpInput();
    }

    public override void Attack()
    {

        int numberOfProjectiles = weaponStats.projectilesCount; // Access projectileCount from weaponStats
        float spreadAngle = weaponStats.spread; // Total spread angle


        float angleStep = 0;
        float startAngle = 0;

        if (numberOfProjectiles > 1)
        {
            // Calculate the angle step between each projectile
            angleStep = spreadAngle / (numberOfProjectiles - 1);
            // Calculate the starting angle for the spread
            startAngle = -spreadAngle / 2;
        }

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate the angle for the current projectile
            float currentAngle = startAngle + (angleStep * i);

            // Calculate the rotation for the projectile
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle)) * projectileSpawnPoint.rotation;

            /* // Instantiate a new projectile at the spawn point with the calculated rotation
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, rotation); */

            // Instantiate a new projectile at the spawn point
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, rotation);

            // Assign the impact effect and damage to the projectile's impact behavior
            ProjectileImpact impactScript = projectile.GetComponent<ProjectileImpact>();
            if (impactScript != null && playerControlReference != null)
            {
                impactScript.Setup(impactEffect, playerControlReference.GetActualDamage(weaponStats.dmg)); // Pass damage to the impact script
            }

            // Add projectile movement (direction and speed)
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate the direction of the projectile
                Vector2 direction = rotation * Vector2.right;  // Use 'right' instead of 'up' for forward direction

                // Apply velocity to move the projectile forward
                rb.linearVelocity = direction * weaponStats.projectileSpeed; // Apply speed to the projectile

                // Add range logic
                Destroy(projectile, weaponStats.projectileRange / weaponStats.projectileSpeed); // Destroy the projectile after reaching its range
            }
        }
    }


    protected override bool RotateWeaponTowardCursor()
    {
        // Call the base class method
        bool shouldFlipWeaponSprite = base.RotateWeaponTowardCursor();

        if (shouldFlipWeaponSprite)
        {
            projectileSpawnPoint.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            projectileSpawnPoint.localRotation = Quaternion.identity;
        }
        return shouldFlipWeaponSprite;
    }



}
