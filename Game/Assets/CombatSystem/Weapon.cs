using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponname;
    public float damage;
    public GameObject projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void fire()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Initialize(damage);
        }
    }
}
