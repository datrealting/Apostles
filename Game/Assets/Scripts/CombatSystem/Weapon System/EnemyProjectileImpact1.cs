using UnityEngine;

public class EnemyProjectileImpact : MonoBehaviour
{

    public PlayerControl player;
    private GameObject impactEffect;
    public int damage; // This will store projectile damage

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public void Setup(GameObject effect, int projectileDamage)
    {
        impactEffect = effect;
        damage = projectileDamage; // Set the damage when the projectile is created
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<NPCStats>()?.TakeDamage(damage);  // Use the damage passed from the weapon
                GameObject.Find("Player").GetComponent<PlayerControl>().onStrike?.Invoke(other.gameObject);
                player.TakeDamage(1);

            }

            if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject); // Destroy the projectile after impact
        }
    }


}