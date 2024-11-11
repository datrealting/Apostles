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

