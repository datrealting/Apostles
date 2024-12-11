using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemEffect itemEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerRelicsHeld>().AddRelic(itemEffect);
            Destroy(gameObject);
            itemEffect.Apply(collision.gameObject);         
        }
    }
}
