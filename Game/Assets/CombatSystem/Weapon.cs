using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string weaponName;

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;

    [Min(0.01f)]
    [SerializeField]
    private float fireRate = 0.5f; // Time in seconds between shots
    private float nextFireTime = 0f;

    [SerializeField]
    private float Damage = 10f;

    private WeaponLevelHandler weaponLevelHandler = new WeaponLevelHandler();

    // Update is called once per frame
    void Update()
    {
        HandleFireInput();
        HandleLevelUpInput();
    }



    private void HandleFireInput()
    {
        // Check if the player is pressing the fire button and if the time has passed since the last shot
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            Fire();
            // Set the next fire time
            nextFireTime = Time.time + fireRate;
        }
    }


    private void HandleLevelUpInput()
    {
        //
#if UNITY_EDITOR
        // Check if the player is pressing the L key
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
#endif
    }


    public void Fire()
    {
        // Instantiate a new projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);


        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = Damage;
        }

    }

    private void LevelUp()
    {
        // Increase the weapon level
        weaponLevelHandler.LevelUp();
    }

    private void UpdateFireRate()
    {
        // Increase the fire rate by 10%
        weaponLevelHandler.DecreaseFireRate(ref fireRate);
    }

    private void UpdateDamage()
    {
        // Set the new damage
        weaponLevelHandler.CalculateDamage(ref Damage);
    }
}

