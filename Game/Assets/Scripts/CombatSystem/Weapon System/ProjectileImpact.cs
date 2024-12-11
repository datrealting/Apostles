using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    private GameObject impactEffect;
    public int damage; // This will store projectile damage

    public void Setup(GameObject effect, int projectileDamage)
    {
        impactEffect = effect;
        damage = projectileDamage; // Set the damage when the projectile is created
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<NPCStats>()?.TakeDamage(damage);  // Use the damage passed from the weapon
                GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(other.gameObject);
            }

            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject); // Destroy the projectile after impact
        }
    }
}