using UnityEngine;

public class ProjectileWeapon : Weapon
{


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    



    private WeaponLevelHandler weaponLevelHandler = new WeaponLevelHandler();


    // Update is called once per frame
    void Update()
    {
        HandleAttackInput();
    }


    public override void Attack()
    {

        // Instantiate a new projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);


        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = Damage;
        }

    }


/*
    private void UpdateFireRate()
    {
        weaponLevelHandler.DecreaseFireRate(ref fireRate);
    }

    private void UpdateDamage()
    {
        // Set the new damage
        weaponLevelHandler.CalculateDamage(ref Damage);
    }*/
}

