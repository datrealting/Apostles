using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();

        }

    }



    public void fire()
    {
        // Instantiate a new projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);


    }




}

