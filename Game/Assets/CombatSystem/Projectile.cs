using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;


    void Start()
    {
        // Set the velocity of the projectile
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        // Destroy the projectile when it collides with something
        Debug.Log("Projectile collided with " + collision.gameObject.name);
        Destroy(gameObject);
    }

}
