using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemEffect itemEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            itemEffect.Apply(collision.gameObject);
        }
    }
}
