using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemEffect itemEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        itemEffect.Apply(collision.gameObject);
    }
}
