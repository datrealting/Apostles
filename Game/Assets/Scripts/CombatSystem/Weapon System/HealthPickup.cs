using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with: " + other.name);
        PlayerControl player = other.GetComponent<PlayerControl>();

        if (player != null)
        {
            Debug.Log("Player detected: " + player.name);
            player.Heal(healAmount);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("PlayerStats component not found on: " + other.name);
        }
    }
}
