using UnityEngine;

public class ProjectileWeapon : Weapon
{


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private Transform playerTransform;



    private WeaponLevelHandler weaponLevelHandler = new WeaponLevelHandler();


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackInput();
        AdjustWeaponDirection();
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


    private void AdjustWeaponDirection()
    {
        Vector3 scale = transform.localScale;
        if (playerTransform.localScale.x < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
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

