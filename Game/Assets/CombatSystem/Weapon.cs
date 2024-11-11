using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    [Min(0f)]
    public float fireRate = 0.5f; // Time in seconds between shots
    private float nextFireTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            fire();
            nextFireTime = Time.time + fireRate;
        }

    }



    public void fire()
    {
        // Instantiate a new projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);


    }




}

